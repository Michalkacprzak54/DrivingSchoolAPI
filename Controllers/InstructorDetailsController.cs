using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrivingSchoolAPI.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;


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
                    .ThenInclude(i => i.InstructorEntitlements)
                        .ThenInclude(ie => ie.Entitlement)  
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
                string role = "instructor";
                var instructorId = instructorLogin.IdInstructor;
                var token = GenerateJwtToken(instructorLogin);
                return Ok(new
                {
                    message = "Zalogowano pomyślnie",
                    token,
                    instructorId,
                    role
                    
            });
            }

            // Sprawdzanie poprawności hasła

            return Unauthorized("Niepoprawne hasło.");
        }

        private string GenerateJwtToken(InstructorDetails instructorDetails)
        {

            if (instructorDetails?.Instructor == null)
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

        [HttpPost("addInstructor")]
        public async Task<IActionResult> AddInstructor([FromBody] InstructorDetailsDto request)
        {
            if (request == null)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }

            try
            {
                // Wywołanie procedury składowanej przy użyciu EF
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DodajInstruktor @imie, @nazwisko, @nr_telefonu, @adres_email, @czy_prowadzi_praktyke, @czy_prowadzi_teorie, @data_urodzenia, @haslo, @pesel, @miasto, @kod_pocztowy, @ulica, @nr_domu, @nr_lokalu",
                    new SqlParameter("@imie", request.InstructorFirstName),
                    new SqlParameter("@nazwisko", request.InstructorLastName),
                    new SqlParameter("@nr_telefonu", request.InstructorPhoneNumber),
                    new SqlParameter("@adres_email", request.InstructorEmail),
                    new SqlParameter("@czy_prowadzi_praktyke", request.InstructorTeachesPractice),
                    new SqlParameter("@czy_prowadzi_teorie", request.InstructorTeachesTheory),
                    new SqlParameter("@data_urodzenia", request.InstructorDateOfBirth),
                    new SqlParameter("@haslo", BCrypt.Net.BCrypt.HashPassword(request.InstructorPassword)), // Hashowanie hasła
                    new SqlParameter("@pesel", request.InstructorPesel),
                    new SqlParameter("@miasto", request.InstructorCity),
                    new SqlParameter("@kod_pocztowy", request.InstructorZipCode),
                    new SqlParameter("@ulica", request.InstructorStreet),
                    new SqlParameter("@nr_domu", request.InstructorHouseNumber),
                    new SqlParameter("@nr_lokalu", (object?)request.InstructorFlatNumber ?? DBNull.Value)
                );

                if (result >= 0)
                {
                    return Ok("Instruktor został pomyślnie dodany.");
                }

                return BadRequest("Nie udało się dodać instruktora.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }


        [HttpPost("schedule")]
        public async Task<IActionResult> AddSchedule([FromBody] ScheduleRequest scheduleRequest)
        {
            if(scheduleRequest == null)
            {
                return BadRequest("Payload is empty or invalid.");
            }
            try
            {
                var execSql = @"
                        EXEC AddHarmonogram 
                        @id_instruktor = @InstructorId, 
                        @data_poczatkowa = @StartDate, 
                        @data_koncowa = @EndDate, 
                        @godzina_rozpoczecia = @StartTime, 
                        @godzina_zakonczenia = @EndTime, 
                        @grupa = @Group,
                        @typ = @Type";

                var parameters = new[]
                {
                        new SqlParameter("@InstructorId", SqlDbType.Int) { Value = scheduleRequest.InstructorId },
                        new SqlParameter("@StartDate", SqlDbType.Date) { Value = scheduleRequest.StartDate },
                        new SqlParameter("@EndDate", SqlDbType.Date) { Value = scheduleRequest.EndDate },
                        new SqlParameter("@StartTime", SqlDbType.Time) { Value = scheduleRequest.StartTime },
                        new SqlParameter("@EndTime", SqlDbType.Time) { Value = scheduleRequest.EndTime },
                        new SqlParameter("@Group", SqlDbType.NVarChar) { Value = (object?)scheduleRequest.Group ?? DBNull.Value },
                        new SqlParameter("@Type", SqlDbType.NVarChar) { Value = scheduleRequest.Type }
                    };

                await _context.Database.ExecuteSqlRawAsync(execSql, parameters);
                return Ok("Schedule added successfully.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        //[HttpPut("Edit/{id}")]
        //public async Task<IActionResult> EditClient(int id, [FromBody] InstructorDetailsDto editRequest)
        //{
        //    if (editRequest == null || id <= 0)
        //    {
        //        return BadRequest("Nieprawidłowe dane do edycji.");
        //    }

        //    try
        //    {
        //        // Weryfikacja, czy klient istnieje
        //        var existingClient = await _context.Instructors.FindAsync(id);
        //        if (existingClient == null)
        //        {
        //            return NotFound("Instruktor nie istnieje.");
        //        }
        //        string newPassword = BCrypt.Net.BCrypt.HashPassword(editRequest.InstructorPassword); 
        //        // Wywołanie procedury składowanej EdytujKlient
        //        var result = await _context.Database.ExecuteSqlRawAsync(
        //            "EXEC AktualizujInstruktor @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7,@p8",
        //            id,                                      
        //            editRequest.InstructorEmail,
        //            newPassword,                    
        //            editRequest.InstructorStreet,                    
        //            editRequest.InstructorHouseNumber,                 
        //            editRequest.InstructorFlatNumber,                     
        //            editRequest.InstructorPhoneNumber,                
        //            editRequest.InstructorCity,         
        //            editRequest.InstructorZipCode
        //        );

        //        if (result >= 0)
        //        {
        //            return NoContent(); // Edycja zakończona sukcesem
        //        }
        //        else
        //        {
        //            return StatusCode(500, "Błąd podczas edycji instruktora.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Błąd serwera: {ex.Message}");
        //    }
        //}


    }
}
