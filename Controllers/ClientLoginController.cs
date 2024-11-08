using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLoginController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientLoginController(DataContext context)
        {
            _context= context;
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

            if (clientLogin == null)
            {
                return Unauthorized("Nie znaleziono użytkownika.");
            }

            // Sprawdzanie poprawności hasła
            if (clientLogin.ClientPassword != loginRequest.Password)
            {
                return Unauthorized("Niepoprawne hasło.");
            }

            // Zwrócenie danych logowania (lub tokenu)
            return Ok(new { message = "Zalogowano pomyślnie", clientId = clientLogin.IdClient });
        }
    }
}
