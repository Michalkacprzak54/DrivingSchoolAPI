using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestController : ControllerBase
    {

        private readonly DataContext _context;

        public ContactRequestController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ContactRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> GetContactRequests()
        {
            return await _context.ContactRequests.ToListAsync();
        }

        // GET: api/ContactRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactRequest>> GetContactRequest(int id)
        {
            var contactRequest = await _context.ContactRequests.FindAsync(id);

            if (contactRequest == null)
            {
                return NotFound();
            }

            return contactRequest;
        }
        // POST: api/ContactRequest
        [HttpPost]
        public async Task<ActionResult<ContactRequest>> PostContactRequest(ContactRequest contactRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Walidacja danych
            }

            _context.ContactRequests.Add(contactRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactRequest", new { id = contactRequest.IdContactRequest }, contactRequest);
        }
    }
}
