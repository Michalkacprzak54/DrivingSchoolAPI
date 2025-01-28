using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingSchoolAPI.Data;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantServiceController : ControllerBase
    {
        private readonly DataContext _context;

        public VariantServiceController(DataContext context)
        {
            _context = context;
        }

        // GET: api/VariantService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VariantService>>> GetVariantServices()
        {
            return await _context.VariantServices.ToListAsync();
        }

        // GET: api/VariantService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariantService>> GetVariantService(int id)
        {
            var variantService = await _context.VariantServices.FindAsync(id);

            if (variantService == null)
            {
                return NotFound();
            }

            return variantService;
        }

        // POST: api/VariantService
        [HttpPost]
        public async Task<ActionResult<VariantService>> PostVariantService(VariantService variantService)
        {
            _context.VariantServices.Add(variantService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVariantService", new { id = variantService.IdVariantService }, variantService);
        }

        // PUT: api/VariantService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariantService(int id, VariantService variantService)
        {
            if (id != variantService.IdVariantService)
            {
                return BadRequest();
            }

            _context.Entry(variantService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariantServiceExists(id))
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

        // DELETE: api/VariantService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariantService(int id)
        {
            var variantService = await _context.VariantServices.FindAsync(id);
            if (variantService == null)
            {
                return NotFound();
            }

            _context.VariantServices.Remove(variantService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VariantServiceExists(int id)
        {
            return _context.VariantServices.Any(e => e.IdVariantService == id);
        }
    }
}
