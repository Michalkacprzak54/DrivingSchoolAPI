using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using System.Net.Sockets;
using DrivingSchoolAPI.Dtos;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientServiceController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientServiceController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ClientServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientServiceDto>>> GetClientServices()
        {
            var clientServices = await _context.ClientServices
                .Include(cs => cs.Service)
                .Include(cs => cs.Client)
                .ToListAsync();

            var clientServiceDtos = clientServices.Select(cs => new ClientServiceDto
            {
                IdClientService = cs.IdClientService,
                Service = new ServiceDto
                {
                    IdService = cs.Service.IdService,
                    ServiceName = cs.Service.ServiceName
                },
                Client = new ClientDto
                {
                    IdClient = cs.Client.IdClient,
                    ClientFirstName = cs.Client.ClientFirstName,
                    ClientLastName = cs.Client.ClientLastName
                },
                
                PurchaseDate = cs.PurchaseDate,  // Użyj PurchaseDate
                Quantity = cs.Quantity,            // Użyj Quantity
                Status = cs.Status                 // Użyj Status
            }).ToList();

            return Ok(clientServiceDtos);
        }

        // GET: api/ClientServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientService>> GetClientService(int id)
        {
            var clientService = await _context.ClientServices.FindAsync(id);

            if (clientService == null)
            {
                return NotFound();
            }

            return clientService;
        }

        // PUT: api/ClientServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientService(int id, ClientService clientService)
        {
            if (id != clientService.IdClientService)
            {
                return BadRequest();
            }

            _context.Entry(clientService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientServiceExists(id))
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

        // POST: api/ClientServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientService>> PostClientService(ClientService clientService)
        {
            _context.ClientServices.Add(clientService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientService", new { id = clientService.IdClientService }, clientService);
        }

        // DELETE: api/ClientServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientService(int id)
        {
            var clientService = await _context.ClientServices.FindAsync(id);
            if (clientService == null)
            {
                return NotFound();
            }

            _context.ClientServices.Remove(clientService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientServiceExists(int id)
        {
            return _context.ClientServices.Any(e => e.IdClientService == id);
        }
    }
}
