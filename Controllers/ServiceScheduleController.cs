using System;
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
                .Include(ss => ss.ClientService)
                .Include(ss => ss.PraticeSchedule)
                .Include(ss => ss.Status)
                .ToListAsync();

            return Ok(servicesSchedule);
        }

    }
}
