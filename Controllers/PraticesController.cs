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
    public class PraticesController : ControllerBase
    {
        private readonly DataContext _context;

        public PraticesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Pratices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pratice>>> GetPratices()
        {
            return await _context.Pratices.ToListAsync();
        }

        // GET: api/Pratices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pratice>> GetPratice(int id)
        {
            var pratice = await _context.Pratices.FindAsync(id);

            if (pratice == null)
            {
                return NotFound();
            }

            return pratice;
        }

        // PUT: api/Pratices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPratice(int id, Pratice pratice)
        {
            if (id != pratice.IdPratice)
            {
                return BadRequest();
            }

            _context.Entry(pratice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PraticeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pratices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pratice>> PostPratice(Pratice pratice)
        {
            if (!ModelState.IsValid)
            {
                // Jeśli model nie jest poprawny, zwracamy odpowiedź z błędami walidacji
                return BadRequest(ModelState);
            }

            try
            {
                _context.Pratices.Add(pratice);
                await _context.SaveChangesAsync();

                // Zwracamy status 201 Created z informacjami o nowo utworzonym zasobie
                return CreatedAtAction("GetPratice", new { id = pratice.IdPratice }, pratice);
            }
            catch (Exception ex)
            {
                // Logujemy błąd i zwracamy odpowiedź serwera 500
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas zapisywania danych.");
            }
        }

        // DELETE: api/Pratices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePratice(int id)
        {
            var pratice = await _context.Pratices.FindAsync(id);
            if (pratice == null)
            {
                return NotFound();
            }

            _context.Pratices.Remove(pratice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PraticeExists(int id)
        {
            return _context.Pratices.Any(e => e.IdPratice == id);
        }
    }
}
