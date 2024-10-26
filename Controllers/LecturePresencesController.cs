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
    public class LecturePresencesController : ControllerBase
    {
        private readonly DataContext _context;

        public LecturePresencesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LecturePresences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturePresence>>> GetLecturePresences()
        {
            return await _context.LecturePresences.ToListAsync();
        }

        // GET: api/LecturePresences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturePresence>> GetLecturePresence(int id)
        {
            var lecturePresence = await _context.LecturePresences.FindAsync(id);

            if (lecturePresence == null)
            {
                return NotFound();
            }

            return lecturePresence;
        }

        // PUT: api/LecturePresences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturePresence(int id, LecturePresence lecturePresence)
        {
            if (id != lecturePresence.IdLecturePresence)
            {
                return BadRequest();
            }

            _context.Entry(lecturePresence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturePresenceExists(id))
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

        // POST: api/LecturePresences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LecturePresence>> PostLecturePresence(LecturePresence lecturePresence)
        {
            _context.LecturePresences.Add(lecturePresence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturePresence", new { id = lecturePresence.IdLecturePresence }, lecturePresence);
        }

        // DELETE: api/LecturePresences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturePresence(int id)
        {
            var lecturePresence = await _context.LecturePresences.FindAsync(id);
            if (lecturePresence == null)
            {
                return NotFound();
            }

            _context.LecturePresences.Remove(lecturePresence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturePresenceExists(int id)
        {
            return _context.LecturePresences.Any(e => e.IdLecturePresence == id);
        }
    }
}
