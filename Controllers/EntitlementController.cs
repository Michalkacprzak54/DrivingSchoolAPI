using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitlementController : ControllerBase
    {
        private readonly DataContext _context;
        public EntitlementController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entitlement>>> GetEntitlements()
        {
            var entitlements = await _context.Entitlements.ToListAsync();
            return Ok(entitlements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Entitlement>>> GetEntitlement(int id)
        {
            var entitlement = 
                await _context.Entitlements
                .Where(e => e.IdEntitlement == id)
                .FirstAsync();
            return Ok(entitlement);
        }

    }
}
