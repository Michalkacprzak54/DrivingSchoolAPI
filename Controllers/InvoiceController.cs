using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoiceController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvocies()
        {
            var invocies = await _context.Invocies
                .Include(i => i.Payments)
                .ToListAsync();
            return Ok(invocies);
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invocies
                .Include(i => i.InvoviceItems)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.IdInvocie == id); 

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Dane płatności są wymagane.");
            }

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DodajPlatnosc @id_faktura, @data_platnosci, @kwota, @metoda_platnosci",
                    new SqlParameter("@id_faktura", paymentRequest.InvoiceId),
                    new SqlParameter("@data_platnosci", paymentRequest.Date),
                    new SqlParameter("@kwota", paymentRequest.Amount),
                    new SqlParameter("@metoda_platnosci", paymentRequest.Method)
                );

                return Ok("Płatność została dodana.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd podczas dodawania płatności: {ex.Message}");
            }
        }


    }
}
