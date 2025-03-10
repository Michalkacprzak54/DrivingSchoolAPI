﻿using System;
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
    public class PraticeSchedulesController : ControllerBase
    {
        private readonly DataContext _context;

        public PraticeSchedulesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PraticeSchedule>>> GetPraticeSchedules()
        {
            return await _context.PraticeSchedules
                .Include(ps => ps.Instructor)
                    .ThenInclude(i => i.InstructorEntitlements)
                        .ThenInclude(ie => ie.Entitlement)
                .ToListAsync();
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<PraticeSchedule>> GetPraticeScheduleById(int id)
        {
            var praticeSchedule = await _context.PraticeSchedules
                .Include(ps => ps.Instructor)
                .Where(ps => ps.IdInstructor == id).ToListAsync();

            if (praticeSchedule == null)
            {
                return NotFound();
            }

            return Ok(praticeSchedule);
        }

        [HttpGet("category/{Category}")]
        public async Task<ActionResult<IEnumerable<PraticeSchedule>>> GetPraticeScheduleByCategory(string Category)
        {
            var praticeSchedule = await _context.PraticeSchedules
                .Include(ps => ps.Instructor)
                    .ThenInclude(i => i.InstructorEntitlements)
                        .ThenInclude(ie => ie.Entitlement)
                .Where(ps => ps.Instructor.InstructorEntitlements.Any(ie => ie.Entitlement.EntitlementName == Category))
                .ToListAsync();

            if (!praticeSchedule.Any())
            {
                return NotFound();
            }

            return Ok(praticeSchedule);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPraticeSchedule(int id, PraticeSchedule praticeSchedule)
        {
            if (id != praticeSchedule.IdPraticeSchedule)
            {
                return BadRequest();
            }

            _context.Entry(praticeSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PraticeScheduleExists(id))
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

        // POST: api/PraticeSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PraticeSchedule>> PostPraticeSchedule(PraticeSchedule praticeSchedule)
        {
            _context.PraticeSchedules.Add(praticeSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPraticeScheduleById", new { id = praticeSchedule.IdPraticeSchedule }, praticeSchedule);
        }

        // DELETE: api/PraticeSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePraticeSchedule(int id)
        {
            var praticeSchedule = await _context.PraticeSchedules.FindAsync(id);
            if (praticeSchedule == null)
            {
                return NotFound();
            }

            _context.PraticeSchedules.Remove(praticeSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PraticeScheduleExists(int id)
        {
            return _context.PraticeSchedules.Any(e => e.IdPraticeSchedule == id);
        }
    }
}
