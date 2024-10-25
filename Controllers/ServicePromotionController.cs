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
    public class ServicePromotionController : ControllerBase
    {
        private readonly DataContext _context;

        public ServicePromotionController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ServicePromotion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePromotion>>> GetServicePromotions()
        {
            var servicePromotions = await _context.ServicePromotions
                .Include(s => s.Service)
                .Include(p => p.Promotion)
                .ToListAsync();
            return Ok(servicePromotions);
        }

        // GET: api/ServicePromotion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePromotion>> GetServicePromotion(int id)
        {
            var servicePromotion = await _context.ServicePromotions.FindAsync(id);

            if (servicePromotion == null)
            {
                return NotFound();
            }

            return servicePromotion;
        }

        // PUT: api/ServicePromotion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicePromotion(int id, ServicePromotion servicePromotion)
        {
            if (id != servicePromotion.IdServicePromotion)
            {
                return BadRequest();
            }

            _context.Entry(servicePromotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePromotionExists(id))
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

        // POST: api/ServicePromotion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServicePromotion>> PostServicePromotion(ServicePromotion servicePromotion)
        {
            _context.ServicePromotions.Add(servicePromotion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicePromotion", new { id = servicePromotion.IdServicePromotion }, servicePromotion);
        }

        // DELETE: api/ServicePromotion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicePromotion(int id)
        {
            var servicePromotion = await _context.ServicePromotions.FindAsync(id);
            if (servicePromotion == null)
            {
                return NotFound();
            }

            _context.ServicePromotions.Remove(servicePromotion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicePromotionExists(int id)
        {
            return _context.ServicePromotions.Any(e => e.IdServicePromotion == id);
        }
    }
}
