using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using DrivingSchoolAPI.Dtos;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            var clients = await _context.Clients
                .Include(c => c.City)
                .Include(c => c.ZipCode)
                .Include(c => c.ClientLogin)
                .ToListAsync(); 

            return Ok(clients);
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.City)
                .Include(c => c.ZipCode)
                //.Include(c => c.ClientServices)
                //    .ThenInclude(cs => cs.Service)
                .FirstAsync(c => c.IdClient == id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpGet("Data/{id}")]
        public async Task<ActionResult<Client>> GetClientData(int id)
        {
            var client = await _context.Clients
                .Include(c => c.City)
                .Include(c => c.ZipCode)
                .Include(c => c.ClientLogin)
                .FirstAsync();

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditClient(int id, [FromBody] ClientDataDto editRequest)
        {
            if (editRequest == null || id <= 0)
            {
                return BadRequest("Nieprawidłowe dane do edycji.");
            }

            try
            {
                // Weryfikacja, czy klient istnieje
                var existingClient = await _context.Clients.FindAsync(id);
                if (existingClient == null)
                {
                    return NotFound("Klient nie istnieje.");
                }

                // Wywołanie procedury składowanej EdytujKlient
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC EdytujKlient @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9",
                    id,                                      // @p0
                    editRequest.FirstName,                   // @p1
                    editRequest.LastName,                    // @p2
                    editRequest.BirthDay,                    // @p3
                    editRequest.PhoneNumber,                 // @p4
                    editRequest.ZipCode,                     // @p5
                    editRequest.City,                        // @p6
                    editRequest.Street,                      // @p7
                    editRequest.HouseNumber,                 // @p8
                    editRequest.FlatNumber                   // @p9
                );

                if (result >= 0)
                {
                    return NoContent(); // Edycja zakończona sukcesem
                }
                else
                {
                    return StatusCode(500, "Błąd podczas edycji klienta.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }



        [HttpPost("Register")]
        public async Task<ActionResult<Client>> RegisterClient([FromBody] ClientDataDto registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Dane rejestracji są niepoprawne.");
            }

            // Sprawdzenie, czy klient z takim e-mailem już istnieje
            var existingClient = await _context.Clients
                .SingleOrDefaultAsync(c => c.ClientLogin.ClientEmail == registerRequest.Email);

            if (existingClient != null)
            {
                return BadRequest("Ten adres e-mail jest już używany.");
            }

            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            try
            {
                // Wykonanie procedury składowanej DodajKlient
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DodajKlient @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10",
                    registerRequest.FirstName,            // @p0
                    registerRequest.LastName,             // @p1
                    registerRequest.BirthDay,             // @p2
                    registerRequest.PhoneNumber,          // @p3
                    registerRequest.Email,                // @p4
                    encryptedPassword,                    // @p5
                    registerRequest.ZipCode,              // @p6
                    registerRequest.City,                 // @p7
                    registerRequest.Street,                 // @p7
                    registerRequest.HouseNumber,          // @p8
                    registerRequest.FlatNumber           // @p9
                );

                // Sprawdzenie wyniku wykonania procedury
                if (result >= 0) // Jeśli procedura zakończyła się sukcesem
                {
                    // Możesz teraz pobrać ID klienta, jeśli chcesz go zwrócić
                    var client = await _context.Clients
                        .FirstOrDefaultAsync(c => c.ClientLogin.ClientEmail == registerRequest.Email);

                    if (client == null)
                    {
                        return StatusCode(500, "Błąd podczas dodawania klienta.");
                    }

                    // Zwrócenie statusu 201 z danymi nowo dodanego klienta
                    return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
                }
                else
                {
                    return StatusCode(500, "Błąd podczas dodawania klienta.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            // Wywołanie procedury składowanej, aby zaktualizować powiązane rekordy
            var sql = "EXEC UsunKlient @id_klient = {0}";
            await _context.Database.ExecuteSqlRawAsync(sql, id);

        

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClient == id);
        }
    }
}
