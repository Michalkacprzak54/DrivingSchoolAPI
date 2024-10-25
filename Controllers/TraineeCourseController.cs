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
    public class TraineeCourseController : ControllerBase
    {
        private readonly DataContext _context;

        public TraineeCourseController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TraineeCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraineeCourse>>> GetTraineeCourses()
        {
            var TraineeCourses = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.Service)
                .Include(tc => tc.Status)
                .ToListAsync();

            return Ok(TraineeCourses);
        }

        // GET: api/TraineeCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeCourse>> GetTraineeCourse(int id)
        {
            var traineeCourse = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.Service)
                .Include(tc => tc.Status)
                .FirstOrDefaultAsync(i => i.IdClient == id);

            if (traineeCourse == null)
            {
                return NotFound();
            }

            return traineeCourse;
        }

        // PUT: api/TraineeCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraineeCourse(int id, TraineeCourse traineeCourse)
        {
            if (id != traineeCourse.IdTraineeCourse)
            {
                return BadRequest();
            }

            _context.Entry(traineeCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeCourseExists(id))
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

        // POST: api/TraineeCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraineeCourse>> PostTraineeCourse(TraineeCourse traineeCourse)
        {
            _context.TraineeCourses.Add(traineeCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraineeCourse", new { id = traineeCourse.IdTraineeCourse }, traineeCourse);
        }

        // DELETE: api/TraineeCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraineeCourse(int id)
        {
            var traineeCourse = await _context.TraineeCourses.FindAsync(id);
            if (traineeCourse == null)
            {
                return NotFound();
            }

            _context.TraineeCourses.Remove(traineeCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraineeCourseExists(int id)
        {
            return _context.TraineeCourses.Any(e => e.IdTraineeCourse == id);
        }
    }
}
