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

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeCourseController : ControllerBase
    {
        private readonly DataContext _context;

        public TraineeCourseController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TraineeCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraineeCourseDto>>> GetTraineeCourses()
        {
            var traineeCourses = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.Service)
                .Include(tc => tc.Status)
                .Select(tc => new TraineeCourseDto
                {
                    IdTraineeCourse = tc.IdTraineeCourse,
                    Client = new ClientDto
                    {
                        IdClient = tc.Client.IdClient,
                        ClientFirstName = tc.Client.ClientFirstName,
                        ClientLastName = tc.Client.ClientLastName
                    },
                    Service = new ServiceDto
                    {
                        IdService = tc.Service.IdService,
                        ServiceName = tc.Service.ServiceName
                    },
                    StartDate = tc.StartDate,
                    EndDate = tc.EndDate,
                    Status = tc.Status.StatusName,
                    PESEL = tc.PESEL,
                    PKK = tc.PKK,
                    MedicalCheck = tc.MedicalCheck,
                    Notes = tc.Notes
                })
                .ToListAsync();

            return Ok(traineeCourses);
        }


        // GET: api/TraineeCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeCourseDto>> GetTraineeCourseById(int id)
        {
            var traineeCourse = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.Service)
                .Include(tc => tc.Status)
                .FirstOrDefaultAsync(tc => tc.IdTraineeCourse == id);

            if (traineeCourse == null)
            {
                return NotFound();
            }

            var traineeCourseDto = new TraineeCourseDto
            {
                IdTraineeCourse = traineeCourse.IdTraineeCourse,
                Client = new ClientDto
                {
                    IdClient = traineeCourse.Client.IdClient,
                    ClientFirstName = traineeCourse.Client.ClientFirstName,
                    ClientLastName = traineeCourse.Client.ClientLastName
                },
                Service = new ServiceDto
                {
                    IdService = traineeCourse.Service.IdService,
                    ServiceName = traineeCourse.Service.ServiceName
                },
                StartDate = traineeCourse.StartDate,
                EndDate = traineeCourse.EndDate,
                Status = traineeCourse.Status.StatusName,
                PESEL = traineeCourse.PESEL,
                PKK = traineeCourse.PKK,
                MedicalCheck = traineeCourse.MedicalCheck,
                Notes = traineeCourse.Notes
            };

            return Ok(traineeCourseDto);
        }

        // PUT: api/TraineeCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraineeCourse(int id, TraineeCourse traineeCourse)
        {
            if (id != traineeCourse.IdTraineeCourse)
            {
                return BadRequest();
            }

            _context.Entry(traineeCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeCourseExists(id))
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

        // POST: api/TraineeCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraineeCourse>> PostTraineeCourse(TraineeCourse traineeCourse)
        {
            _context.TraineeCourses.Add(traineeCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraineeCourse", new { id = traineeCourse.IdTraineeCourse }, traineeCourse);
        }

        // DELETE: api/TraineeCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraineeCourse(int id)
        {
            var traineeCourse = await _context.TraineeCourses.FindAsync(id);
            if (traineeCourse == null)
            {
                return NotFound();
            }

            _context.TraineeCourses.Remove(traineeCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraineeCourseExists(int id)
        {
            return _context.TraineeCourses.Any(e => e.IdTraineeCourse == id);
        }
    }
}
