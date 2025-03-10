﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using DrivingSchoolAPI.Dtos;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceScheduleController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceScheduleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceSchedule>>> GetServicesSchedule()
        {
            var servicesSchedule = await _context.ServiceSchedules
                .ToListAsync();

            return Ok(servicesSchedule);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ServiceSchedule>>> GetServiceSchedule(int id)
        {
            var servicesSchedule = await _context.ServiceSchedules
                .Include(ss => ss.ClientService)
                .Include(ss => ss.PraticeSchedule)
                .Include(ss => ss.Status)
                .Where(ss => ss.IdServiceSchedule == id)
                .FirstAsync();

            return Ok(servicesSchedule);
        }

        [HttpGet("schedule/{idSchedule}")]
        public async Task<ActionResult<IEnumerable<ServiceSchedule>>> GetServiceScheduleByIdSchedule(int idSchedule)
        {
            var servicesSchedule = await _context.ServiceSchedules
                .Include(ss => ss.ClientService)
                .Include(ss => ss.PraticeSchedule)
                .Include(ss => ss.Status)
                .Where(ss => ss.IdPraticeSchedule == idSchedule)
                .FirstAsync();

            return Ok(servicesSchedule);
        }

        [HttpGet("byClientServiceId/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceSchedule>>> GetServiceScheduleByClientServiceId(int id)
        {
            var servicesSchedule = await _context.ServiceSchedules
                .Include(ss => ss.ClientService)
                .Include(ss => ss.PraticeSchedule)
                    .ThenInclude(ps => ps.Instructor)
                .Include(ss => ss.Status)
                .Where(ss => ss.IdClientService == id)
                .ToListAsync();

            return Ok(servicesSchedule);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceSchedule>> PostServiceSchedule(ServiceSchedule serviceSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC DodajUslugaHarmonogram @IdKlientUsluga = {0}, @IdHarmonogram = {1}, @DataRezerwacji = {2}, @IdStatus = {3}",
                    serviceSchedule.IdClientService,
                    serviceSchedule.IdPraticeSchedule,
                    serviceSchedule.ReservationDate,
                    serviceSchedule.IdStatus
                    );
                return CreatedAtAction("GetServiceSchedule", new { id = serviceSchedule.IdServiceSchedule }, serviceSchedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas zapisywania danych.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceSchedule(int id)
        {
            var serviceSchedule = await _context.ServiceSchedules.FindAsync(id);
            if (serviceSchedule == null)
            {
                return NotFound();
            }

            // Wywołanie procedury składowanej
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteServiceScheduleAndUpdateSchedule @IdUslugaHarmonogram = {0}", id);

            return NoContent();
        }
    }
}
