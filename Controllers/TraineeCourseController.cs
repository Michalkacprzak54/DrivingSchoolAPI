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
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

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
                    Status = new StatusDto
                    {
                        IdStatus = tc.Status.IdStatus
                    },
                    StartDate = tc.StartDate,
                    EndDate = tc.EndDate,
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
                Status = new StatusDto
                {
                    IdStatus = traineeCourse.Status.IdStatus
                },
                StartDate = traineeCourse.StartDate,
                EndDate = traineeCourse.EndDate,
                PESEL = traineeCourse.PESEL,
                PKK = traineeCourse.PKK,
                MedicalCheck = traineeCourse.MedicalCheck,
                Notes = traineeCourse.Notes
            };

            return Ok(traineeCourseDto);
        }

        [HttpPost]
        public async Task<ActionResult<TraineeCourseDto>> PostTraineeCourse([FromBody] TraineeCourseDto createTraineeCourseDto)
        {
            Console.WriteLine($"Otrzymane dane: {JsonConvert.SerializeObject(createTraineeCourseDto)}");  // Zaloguj dane
            if (createTraineeCourseDto == null || createTraineeCourseDto.Client == null)
            {
                return BadRequest("Payload is empty or invalid.");
            }
            var dto = createTraineeCourseDto;
            
            Console.WriteLine($"ClientId: {dto.Client?.IdClient}, ServiceId: {dto.Service?.IdService}, Quantity: {dto.StartDate}");
            

            try
            {
                // Tworzymy parametr wyjściowy na komunikat
                var resultMessage = new SqlParameter("@resultMessage", SqlDbType.NVarChar, 4000) { Direction = ParameterDirection.Output };

                // Definiujemy parametry wejściowe dla procedury składowanej
                var parameters = new[]
                {
                    new SqlParameter("@IdClient", SqlDbType.Int) { Value = createTraineeCourseDto.Client.IdClient},
                    new SqlParameter("@IdService", SqlDbType.Int) { Value = createTraineeCourseDto.Service.IdService},
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = createTraineeCourseDto.StartDate },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = createTraineeCourseDto.EndDate ?? (object)DBNull.Value},
                    new SqlParameter("@IdStatus", SqlDbType.Int) { Value = createTraineeCourseDto.Status.IdStatus},
                    new SqlParameter("@PESEL", SqlDbType.NVarChar, 11) { Value = createTraineeCourseDto.PESEL},
                    new SqlParameter("@PKK", SqlDbType.NVarChar, 26) { Value = createTraineeCourseDto.PKK},
                    new SqlParameter("@MedicalCheck", SqlDbType.Bit) { Value = createTraineeCourseDto.MedicalCheck},
                    new SqlParameter("@Notes", SqlDbType.Text) { Value = createTraineeCourseDto.Notes ?? (object)DBNull.Value },
                    resultMessage
                };

                // Wykonujemy procedurę składowaną
                var execSql = @"
                    EXEC DodajKursantKurs 
                        @IdClient, 
                        @IdService, 
                        @StartDate, 
                        @EndDate, 
                        @IdStatus, 
                        @PESEL, 
                        @PKK, 
                        @MedicalCheck, 
                        @Notes, 
                        @resultMessage OUTPUT;";

                // Wywołujemy procedurę składowaną
                await _context.Database.ExecuteSqlRawAsync(execSql, parameters);

                // Możesz logować komunikat wynikowy
                Console.WriteLine($"Result: {resultMessage.Value}");

                // Jeśli procedura zakończyła się sukcesem, zwracamy wynik
                return Ok(new { Message = resultMessage.Value });
            }
            catch (Exception ex)
            {
                // Obsługujemy wyjątki
                return Problem(detail: ex.Message, statusCode: 500);
            }
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
