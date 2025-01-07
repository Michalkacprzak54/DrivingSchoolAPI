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

        // PUT: api/TheorySchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheorySchedule(int id, TheorySchedule theorySchedule)
        {
            if (id != theorySchedule.IdTheorySchedule)
            {
                return BadRequest();
            }

            _context.Entry(theorySchedule).State = EntityState.Modified;

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

        // POST: api/TheorySchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TheorySchedule>> PostTheorySchedule(TheorySchedule theorySchedule)
        {
            _context.TheorySchedules.Add(theorySchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheorySchedule", new { id = theorySchedule.IdTheorySchedule }, theorySchedule);
        }

        // DELETE: api/TheorySchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheorySchedule(int id)
        {
            var theorySchedule = await _context.TheorySchedules.FindAsync(id);
            if (theorySchedule == null)
            {
                return NotFound();
            }

            _context.TheorySchedules.Remove(theorySchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TheoryScheduleExists(int id)
        {
            return _context.TheorySchedules.Any(e => e.IdTheorySchedule == id);
        }
    }
}
