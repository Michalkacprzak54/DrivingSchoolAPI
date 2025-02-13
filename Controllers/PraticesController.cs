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
        public async Task<ActionResult<IEnumerable<Pratice>>> GetPratice(int id)
        {
            var userPratice = await _context.Pratices
                .Where(p => p.IdCourseDetails == id).ToListAsync();

            if (userPratice == null)
            {
                return NotFound();
            }

            return Ok(userPratice);
        }

        [HttpGet("schedule/{id}")]
        public async Task<ActionResult<IEnumerable<Pratice>>> GetPraticesByIdSchedule(int id)
        {
            var userPratice = await _context.Pratices
                .Where(p => p.IdPraticeSchedule == id).FirstAsync();

            if (userPratice == null)
            {
                return NotFound();
            }

            return Ok(userPratice);
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
                return BadRequest(ModelState);
            }

            try
            {
                // Wywołanie procedury składowanej
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddPraticeAndUpdateSchedule @IdHarmonogram = {0}, @IdSzczegoly = {1}, @DataRezerwacji = {2}, @IdStatus = {3}",
                    pratice.IdPraticeSchedule,
                    pratice.IdCourseDetails,
                    pratice.ReservationDate,
                    pratice.IdStatus
                );

                // Możesz zwrócić odpowiedź z danymi, jeśli chcesz
                return Ok(pratice);

            }
            catch (Exception ex)
            {
                // Logowanie błędu i zwrócenie odpowiedzi 500
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas zapisywania danych.");
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdatePratice(int id, [FromBody] Pratice Pratice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Wywołanie procedury składowanej
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdatePraktyka @id_harmonogram_praktyka = {0}, @NewDataPraktyk = {1}, @NewGodzinaRozpoczecia = {2}, @NewGodzinaZakonczenia = {3}, @NewIdStatus = {4}",
                    id,
                    Pratice.PraticeDate,
                    Pratice.StartHour,
                    Pratice.EndHour,
                    Pratice.IdStatus
                );

                return Ok(201); // 204 No Content, jeśli operacja się powiedzie
            }
            catch (Exception ex)
            {
                // Logowanie błędu i zwrócenie odpowiedzi 500
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas aktualizacji danych.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePratice(int id)
        {
            var pratice = await _context.Pratices.FindAsync(id);
            if (pratice == null)
            {
                return NotFound();
            }

            // Wywołanie procedury składowanej
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeletePracticeAndUpdateSchedule @IdPraktyka = {0}", id);

            return NoContent();
        }


        private bool PraticeExists(int id)
        {
            return _context.Pratices.Any(e => e.IdPratice == id);
        }
    }
}
