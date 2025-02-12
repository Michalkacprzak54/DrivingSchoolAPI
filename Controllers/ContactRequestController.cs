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

        // POST: api/ContactRequest
        [HttpPost]
        public async Task<ActionResult<ContactRequest>> PostContactRequest(ContactRequest contactRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            _context.ContactRequests.Add(contactRequest);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut("resolve/{id}")]
        public async Task<IActionResult> MarkRequestAsResolved(int id)
        {
            var contactRequest = await _context.ContactRequests.FindAsync(id);

            if (contactRequest == null)
            {
                return NotFound("Zgłoszenie nie istnieje.");
            }

            // Zmiana statusu zgłoszenia na "załatwione"
            contactRequest.IsCurrent = false;
            _context.Entry(contactRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Zgłoszenie oznaczone jako załatwione.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Błąd podczas aktualizacji zgłoszenia.");
            }
        }
    }
}