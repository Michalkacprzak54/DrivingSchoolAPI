using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;

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
                //.Include(c => c.TraineeCourse)
                .Include(c => c.ClientServices)
                    .ThenInclude(cs => cs.Service)
                .FirstAsync(c => c.IdClient == id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClient(int id, Client client)
        //{
        //    if (id != client.IdClient)
        //    {
        //        return BadRequest("ID w URL nie zgadza się z ID klienta.");
        //    }
        //    try
        //    {
        //        var result = await _context.Database.ExecuteSqlRawAsync(
        //       "EXEC EdytujKlient @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9",
        //       client.IdClient,             //@p11
        //       client.ClientFirstName,            // @p0
        //       client.ClientLastName,        // @p1
        //       client.ClientBirthDay,   // @p2
        //       client.ClientPhoneNumber,      // @p3
        //       client.ZipCode.ZipCodeNumber,     // @p6
        //       client.City.CityName,          // @p7
        //       client.ClientHouseNumber,       // @p8
        //       client.ClientFlatNumber,     // @p9
        //       client.ClientStatus          // @p10
        //       );

        //        // Sprawdzenie wyniku wykonania procedury
        //        if (result >= 0) // Jeśli procedura zakończyła się sukcesem
        //        {
        //            return NoContent();
        //        }
        //        else
        //        {
        //            return StatusCode(500, "Błąd podczas edytowania klienta.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return StatusCode(500, $"Błąd serwera: {ex.Message}");
        //    }
        //}

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
               "EXEC DodajKlient @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10",
               client.ClientFirstName,            // @p0
               client.ClientLastName,        // @p1
               client.ClientBirthDay,        // @p2
               client.ClientPhoneNumber,      // @p3
               client.ClientLogin.ClientEmail,      // @p4
               client.ClientLogin.ClientPassword,      // @p5
               client.ZipCode.ZipCodeNumber,     // @p6
               client.City.CityName,          // @p7
               client.ClientHouseNumber,       // @p8
               client.ClientFlatNumber,     // @p9
               client.ClientStatus          // @p10
           );

            // Sprawdzenie wyniku wykonania procedury
            if (result >= 0) // Jeśli procedura zakończyła się sukcesem
            {
                return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
            }
            else
            {
                return StatusCode(500, "Błąd podczas dodawania klienta.");
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

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClient == id);
        }
    }
}
