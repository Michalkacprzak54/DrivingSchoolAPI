using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorDetailsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public InstructorDetailsController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDetails>>> GetInstructorsDetails()
        {
            return await _context.InstructorDetails
                .Include(id => id.Instructor)
                .Include(id => id.City)
                .Include(id => id.ZipCode)
                .ToListAsync();
        }

        // Pobieranie szczegółów instruktora na podstawie jego ID
        [HttpGet("{idInstructor}")]
        public async Task<ActionResult<InstructorDetails>> GetInstructorDetail(int idInstructor)
        {
            var instructorDetail = await _context.InstructorDetails
                .Include(id => id.Instructor)
                .Include(id => id.City)
                .Include(id => id.ZipCode)
                .FirstOrDefaultAsync(id => id.IdInstructor == idInstructor);

            if (instructorDetail == null)
            {
                return NotFound();  // Jeśli nie znaleziono, zwróć 404 Not Found
            }

            return Ok(instructorDetail);  // Zwróć szczegóły instruktora
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email i hasło są wymagane.");
            }

            // Wyszukiwanie loginu na podstawie e-maila
            var instructorLogin = await _context.InstructorDetails
                .Include(id => id.Instructor)
                .FirstOrDefaultAsync(id => id.Instructor.InstructorEmail == loginRequest.Email);

            if (instructorLogin == null)
            {
                return Unauthorized("Nie znaleziono użytkownika.");
            }


            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, instructorLogin.InstructorPassword);


            if (isPasswordValid)
            {
                var instructorId = instructorLogin.IdInstructor;
                var token = GenerateJwtToken(instructorLogin);
                return Ok(new
                {
                    message = "Zalogowano pomyślnie",
                    token,
                    instructorId
                });
            }

            // Sprawdzanie poprawności hasła

            return Unauthorized("Niepoprawne hasło.");
        }

        private string GenerateJwtToken(InstructorDetails instructorDetails)
        {

            if(instructorDetails?.Instructor == null)
            {
                throw new ArgumentException("Instructor details are incomplete. Instructor information is missing.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, instructorDetails.Instructor.InstructorEmail),
                new Claim("instructorId", instructorDetails.IdInstructor.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", "instructor")  // Dodanie roli instruktora
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
