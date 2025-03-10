﻿using System;
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
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

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
        public async Task<ActionResult<IEnumerable<ClientService>>> GetClientServices()
        {
            var clientServices = await _context.ClientServices
                .Include(cs => cs.Service)
                .Include(cs => cs.VariantService)
                .Include(cs => cs.Client)
                .ToListAsync();

            var clientServiceDtos = clientServices.Select(cs => new ClientServiceDto
            {
                IdClientService = cs.IdClientService,
                Service = new ServiceDto
                {
                    IdService = cs.Service.IdService,
                    ServiceName = cs.Service.ServiceName,
                    ServiceType = cs.Service.ServiceType,
                    ServicePlace = cs.Service.ServicePlace
                },
                Client = new ClientDto
                {
                    IdClient = cs.Client.IdClient,
                    ClientFirstName = cs.Client.ClientFirstName,
                    ClientLastName = cs.Client.ClientLastName
                },
                VariantService = cs.VariantService != null ? cs.VariantService : null,
                PurchaseDate = cs.PurchaseDate,
                Quantity = cs.Quantity,
                Status = cs.Status,
                IsUsed = cs.IsUsed,
                HowManyUsed = cs.HowManyUsed,
            }).ToList();

            return Ok(clientServiceDtos);
        }

        // GET: api/ClientServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ClientServiceDto>>> GetClientService(int id)
        {
            var clientService = await _context.ClientServices
                .Include(cs => cs.Service)
                .Include(cs => cs.VariantService)
                .Include(cs => cs.Client)
                .ToListAsync();

            var clientServiceDto = clientService.Select(cs => new ClientServiceDto
            {
                IdClientService = cs.IdClientService,
                Service = new ServiceDto
                {
                    IdService = cs.Service.IdService,
                    ServiceName = cs.Service.ServiceName,
                    ServiceType = cs.Service.ServiceType,
                    ServicePlace = cs.Service.ServicePlace
                },
                Client = new ClientDto
                {
                    IdClient = cs.Client.IdClient,
                    ClientFirstName = cs.Client.ClientFirstName,
                    ClientLastName = cs.Client.ClientLastName
                },
                VariantService = cs.VariantService != null ? cs.VariantService : null,

                PurchaseDate = cs.PurchaseDate,  
                Quantity = cs.Quantity,            
                Status = cs.Status,
                IsUsed = cs.IsUsed,
                HowManyUsed = cs.HowManyUsed,

            }).Where(cs => cs.Client.IdClient == id).ToList();

            return clientServiceDto;
        }

        [HttpGet("service/{id}")]
        public async Task<ActionResult<ClientServiceDto>> GetClientServiceById(int id)
        {
            var clientService = await _context.ClientServices
                .Include(cs => cs.Service)
                .Include(cs => cs.VariantService)
                .Include(cs => cs.Client)
                .FirstOrDefaultAsync(cs => cs.IdClientService == id);

            if (clientService == null)
            {
                return NotFound();
            }

            var clientServiceDto = new ClientServiceDto
            {
                IdClientService = clientService.IdClientService,
                Service =  new ServiceDto
                {
                    IdService = clientService.Service.IdService,
                    ServiceName = clientService.Service.ServiceName,
                    ServiceType = clientService.Service.ServiceType,
                    ServicePlace = clientService.Service.ServicePlace
                },
                Client = new ClientDto
                {
                    IdClient = clientService.Client.IdClient,
                    ClientFirstName = clientService.Client.ClientFirstName,
                    ClientLastName = clientService.Client.ClientLastName
                },
                VariantService = clientService.VariantService != null ? clientService.VariantService : null,
                PurchaseDate = clientService.PurchaseDate,
                Quantity = clientService.Quantity,
                Status = clientService.Status,
                IsUsed = clientService.IsUsed,
                HowManyUsed = clientService.HowManyUsed,

            };

            return clientServiceDto;
        }




        [HttpPost]
        public async Task<IActionResult> PostClientServices([FromBody] List<ClientServiceDto> clientServiceDtos)
        {
            if (clientServiceDtos == null || !clientServiceDtos.Any())
            {
                return BadRequest("Payload is empty or invalid.");
            }
            foreach (var dto in clientServiceDtos)
            {
                Console.WriteLine($"ClientId: {dto.Client?.IdClient}, ServiceId: {dto.Service?.IdService}, Quantity: {dto.Quantity}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }
            try
            {
                foreach (var clientServiceDto in clientServiceDtos)
                {
                    var execSql = @"
                    EXEC DodajKlientUsluga 
                    @id_klient = @ClientId, 
                    @id_usluga = @ServiceId, 
                    @id_usluga_wariant = @ServiceVariantId,
                    @ilosc = @Quantity,
                    @data_zakupu = @PurchaseDate;";

                    var parameters = new[]
                    {
                        new SqlParameter("@ClientId", SqlDbType.Int) { Value = clientServiceDto.Client.IdClient },
                        new SqlParameter("@ServiceId", SqlDbType.Int) { Value = clientServiceDto.Service.IdService },
                        new SqlParameter("@ServiceVariantId", SqlDbType.Int) { Value = clientServiceDto.VariantService.IdVariantService }, 
                        new SqlParameter("@Quantity", SqlDbType.Int) { Value = clientServiceDto.Quantity },
                        new SqlParameter("@PurchaseDate", SqlDbType.DateTime) { Value = clientServiceDto.PurchaseDate }
                    };

                    await _context.Database.ExecuteSqlRawAsync(execSql, parameters);

                }

                return Ok("All client services have been added successfully.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
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

        private async Task<bool> ClientServiceExists(int id)
        {
            return await _context.ClientServices.AnyAsync(e => e.IdClientService == id);
        }

        // PUT: api/ClientServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClientService(int id, ClientService clientService)
        //{
        //    if (id != clientService.IdClientService)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(clientService).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClientServiceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
    }
}
