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
    public class TheorySchedulesController : ControllerBase
    {
        private readonly DataContext _context;

        public TheorySchedulesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TheorySchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheorySchedule>>> GetTheorySchedules()
        {
            return await _context.TheorySchedules.ToListAsync();
        }

        // GET: api/TheorySchedules/5
        [HttpGet("{idInstructor}")]
        public async Task<ActionResult<TheorySchedule>> GetTheorySchedule(int idInstructor)
        {
            var theorySchedule = await _context.TheorySchedules.Where(ts => ts.IdInsctructor == idInstructor).ToListAsync();

            if (theorySchedule == null)
            {
                return NotFound();
            }

            return Ok(theorySchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheorySchedule(int id, [FromBody] TheorySchedule updatedData)
        {
            var existingSchedule = await _context.TheorySchedules.FindAsync(id);
            if (existingSchedule == null)
            {
                return NotFound();
            }

            // Aktualizujemy tylko idInstructor
            existingSchedule.IdInsctructor = updatedData.IdInsctructor;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheoryScheduleExists(id))
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



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheorySchedule(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Nieprawidłowy ID wykładu.");
            }

            // Wywołanie procedury RemoveHarmonogram
            await _context.Database.ExecuteSqlRawAsync("EXEC RemoveHarmonogram @id_wyklad = {0}", id);

            return NoContent();
        }


        private bool TheoryScheduleExists(int id)
        {
            return _context.TheorySchedules.Any(e => e.IdTheorySchedule == id);
        }
    }
}
