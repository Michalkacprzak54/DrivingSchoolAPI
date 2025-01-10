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
    public class InscrutorEntitlementsController : ControllerBase
    {
        private readonly DataContext _context;
        public InscrutorEntitlementsController(DataContext context)
        {
            _context = context;
        }
        // GET: api/InscrutorEntitlements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscrutorEntitlement>>> GetInscrutorEntitlements()
        {
            var inscrutorEntitlement =
                await _context.InscrutorEntitlements
                .Include(ie => ie.Instructor)
                .Include(ie => ie.Entitlement)
                .ToListAsync();
            return Ok(inscrutorEntitlement);
        }
        // GET: api/InscrutorEntitlements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscrutorEntitlement>> GetInscrutorEntitlement(int id)
        {
            var inscrutorEntitlement = 
                await _context.InscrutorEntitlements
                .Include(ie => ie.Entitlement)
                .Where(ie => ie.IdInstructor == id).ToListAsync();
            if (inscrutorEntitlement == null)
            {
                return NotFound();
            }
            return Ok(inscrutorEntitlement);
        }
        // PUT: api/InscrutorEntitlements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscrutorEntitlement(int id, InscrutorEntitlement inscrutorEntitlement)
        {
            if (id != inscrutorEntitlement.IdInscrutorEntitlement)
            {
                return BadRequest();
            }
            _context.Entry(inscrutorEntitlement).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscrutorEntitlementExists(id))
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
        // POST: api/InscrutorEntitlements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InscrutorEntitlement>> PostInscrutorEntitlement(InscrutorEntitlement inscrutorEntitlement)
        {
            _context.InscrutorEntitlements.Add(inscrutorEntitlement);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInscrutorEntitlement", new { id = inscrutorEntitlement.IdInscrutorEntitlement }, inscrutorEntitlement);
        }
        // DELETE: api/InscrutorEntitlements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscrutorEntitlement(int id)
        {
            var inscrutorEntitlement = await _context.InscrutorEntitlements.FindAsync(id);
            if (inscrutorEntitlement == null)
            {
                return NotFound();
            }
            _context.InscrutorEntitlements.Remove(inscrutorEntitlement);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool InscrutorEntitlementExists(int id)
        {
            return _context.InscrutorEntitlements.Any(e => e.IdInscrutorEntitlement == id);
        }
    }
}