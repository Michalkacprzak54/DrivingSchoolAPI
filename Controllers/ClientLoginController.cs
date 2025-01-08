using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrivingSchoolAPI.Dtos;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLoginController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public ClientLoginController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientLogin(int id)
        {
            var clientLogin = await _context.ClientLogins
                .FirstOrDefaultAsync(cl => cl.IdClientLogin == id);

            if (clientLogin == null)
            {
                return NotFound();
            }

            return Ok(clientLogin);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email i hasło są wymagane.");
            }

            // Wyszukiwanie loginu na podstawie e-maila
            var clientLogin = await _context.ClientLogins
                .FirstOrDefaultAsync(cl => cl.ClientEmail == loginRequest.Email);

            var userId = clientLogin.IdClient;

            if (clientLogin == null)
            {
                return Unauthorized("Nie znaleziono użytkownika.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, clientLogin.ClientPassword);

            if (isPasswordValid)
            {
                string role = "client";
                var token = GenerateJwtToken(clientLogin);
                return Ok(new 
                { message = "Zalogowano pomyślnie",
                  token,
                  userId,
                  role
                });
            }

            // Sprawdzanie poprawności hasła

            return Unauthorized("Niepoprawne hasło.");
        }

        [HttpPut("ChangePassword/{id}")]
        public IActionResult ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var clientLogin = _context.ClientLogins.FirstOrDefault(cl => cl.IdClient == id);
            if (clientLogin == null)
                return NotFound("Użytkownik nie istnieje.");

            // Sprawdzenie poprawności starego hasła
            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, clientLogin.ClientPassword))
                return BadRequest("Stare hasło jest nieprawidłowe.");

            // Aktualizacja hasła
            clientLogin.ClientPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _context.SaveChanges();

            return Ok("Hasło zostało zmienione.");
        }

        private string GenerateJwtToken(ClientLogin clientLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientLogin.ClientEmail),
                new Claim("clientId", clientLogin.IdClient.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
