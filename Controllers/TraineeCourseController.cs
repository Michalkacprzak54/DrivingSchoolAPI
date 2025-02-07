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
using Azure.Core;
using Microsoft.AspNetCore.Mvc.Routing;

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
                .Include(tc => tc.VariantService)
                    .ThenInclude(VariantService => VariantService.Service)
                .Include(tc => tc.Status)
                .Include(tc => tc.CourseDetails)
                .Select(tc => new TraineeCourseDto
                {
                    IdTraineeCourse = tc.IdTraineeCourse,
                    Client = new ClientDto
                    {
                        IdClient = tc.Client.IdClient,
                        ClientFirstName = tc.Client.ClientFirstName,
                        ClientLastName = tc.Client.ClientLastName
                    },
                    VarinatService = tc.VariantService,
                    Service = new ServiceDto
                    {
                        IdService = tc.VariantService.Service.IdService,
                        ServiceName = tc.VariantService.Service.ServiceName,
                        ServicePlace = tc.VariantService.Service.ServicePlace,
                        ServiceCategory = tc.VariantService.Service.ServiceCategory
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
                    ParentalConsent = tc.ParentalConsent,
                    Notes = tc.Notes,
                    CourseDetails = new CourseDetailsDto
                    {
                        IdCourseDetails = tc.CourseDetails.IdCourseDetails,
                        TheoryHoursCount = tc.CourseDetails.TheoryHoursCount,
                        PraticeHoursCount = tc.CourseDetails.PraticeHoursCount,
                        InternalExam = tc.CourseDetails.InternalExam,
                        CreationDate = tc.CourseDetails.CreationDate,
                        Notes = tc.CourseDetails.Notes
                    }
                }).ToListAsync();

            return Ok(traineeCourses);
        }

        [HttpGet("withoutTests")]
        public async Task<ActionResult<IEnumerable<TraineeCourseDto>>> GetTraineeCoursesWithoutTests()
        {
            var traineeCourses = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.VariantService)
                    .ThenInclude(VariantService => VariantService.Service)
                .Include(tc => tc.Status)
                .Include(tc => tc.CourseDetails)
                    .Where(tc => tc.MedicalCheck == false || tc.MedicalCheck == null ||
                    ((tc.ParentalConsent == false || tc.ParentalConsent == null) &&
                    EF.Functions.DateDiffYear(tc.Client.ClientBirthDay, DateOnly.FromDateTime(DateTime.Today)) < 18))



                .Select(tc => new TraineeCourseDto
                {
                    IdTraineeCourse = tc.IdTraineeCourse,
                    Client = new ClientDto
                    {
                        IdClient = tc.Client.IdClient,
                        ClientFirstName = tc.Client.ClientFirstName,
                        ClientLastName = tc.Client.ClientLastName,
                        ClientBirthDay = tc.Client.ClientBirthDay
                    },
                    VarinatService = tc.VariantService,
                    Service = new ServiceDto
                    {
                        IdService = tc.VariantService.Service.IdService,
                        ServiceName = tc.VariantService.Service.ServiceName,
                        ServicePlace = tc.VariantService.Service.ServicePlace,
                        ServiceCategory = tc.VariantService.Service.ServiceCategory
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
                    ParentalConsent = tc.ParentalConsent,
                    Notes = tc.Notes,
                    CourseDetails = new CourseDetailsDto
                    {
                        IdCourseDetails = tc.CourseDetails.IdCourseDetails,
                        TheoryHoursCount = tc.CourseDetails.TheoryHoursCount,
                        PraticeHoursCount = tc.CourseDetails.PraticeHoursCount,
                        InternalExam = tc.CourseDetails.InternalExam,
                        CreationDate = tc.CourseDetails.CreationDate,
                        Notes = tc.CourseDetails.Notes
                    }
                }).ToListAsync();

            return Ok(traineeCourses);
        }

        [HttpGet("uncompletedTrainees")]
        public async Task<ActionResult<IEnumerable<TraineeCourseDto>>> GetUncompletedTrainees()
        {
            var traineeCourses = await _context.TraineeCourses
                .Include(tc => tc.Client)
                .Include(tc => tc.VariantService)
                    .ThenInclude(VariantService => VariantService.Service)
                .Include(tc => tc.Status)
                .Include(tc => tc.CourseDetails)
                 .Where(tc => !tc.CourseDetails.InternalExam 
                    && (tc.CourseDetails.TheoryHoursCount < tc.VariantService.NumberTheoryHours))
                .Select(tc => new TraineeCourseDto
                {
                    IdTraineeCourse = tc.IdTraineeCourse,
                    Client = new ClientDto
                    {
                        IdClient = tc.Client.IdClient,
                        ClientFirstName = tc.Client.ClientFirstName,
                        ClientLastName = tc.Client.ClientLastName
                    },
                    VarinatService = tc.VariantService,
                    Service = new ServiceDto
                    {
                        IdService = tc.VariantService.Service.IdService,
                        ServiceName = tc.VariantService.Service.ServiceName,
                        ServicePlace = tc.VariantService.Service.ServicePlace,
                        ServiceCategory = tc.VariantService.Service.ServiceCategory
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
                    ParentalConsent = tc.ParentalConsent,
                    Notes = tc.Notes,
                    CourseDetails = new CourseDetailsDto
                    {
                        IdCourseDetails = tc.CourseDetails.IdCourseDetails,
                        TheoryHoursCount = tc.CourseDetails.TheoryHoursCount,
                        PraticeHoursCount = tc.CourseDetails.PraticeHoursCount,
                        InternalExam = tc.CourseDetails.InternalExam,
                        CreationDate = tc.CourseDetails.CreationDate,
                        Notes = tc.CourseDetails.Notes
                    }
                }).ToListAsync();

            return Ok(traineeCourses);
        }


        // GET: api/TraineeCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraineeCourseDto>> GetTraineeCourseById(int id)
        {
            var traineeCourses = await _context.TraineeCourses
                .Where(tc => tc.Client.IdClient == id)  
               .Include(tc => tc.Client)
               .Include(tc => tc.VariantService)
                    .ThenInclude(VariantService => VariantService.Service)
               .Include(tc => tc.Status)
               .Include(tc => tc.CourseDetails)  
                    .Where(tc => tc.IdTraineeCourse == id)
               .Select(tc => new TraineeCourseDto
               {
                   IdTraineeCourse = tc.IdTraineeCourse,
                   Client = new ClientDto
                   {
                       IdClient = tc.Client.IdClient,
                       ClientFirstName = tc.Client.ClientFirstName,
                       ClientLastName = tc.Client.ClientLastName
                   },
                   VarinatService = tc.VariantService,
                   Service = new ServiceDto
                   {
                       IdService = tc.VariantService.Service.IdService,
                       ServiceName = tc.VariantService.Service.ServiceName,
                       ServicePlace = tc.VariantService.Service.ServicePlace,
                       ServiceCategory = tc.VariantService.Service.ServiceCategory
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
                   ParentalConsent = tc.ParentalConsent,
                   Notes = tc.Notes,
                   CourseDetails = new CourseDetailsDto
                   {
                       IdCourseDetails = tc.CourseDetails.IdCourseDetails,
                       TheoryHoursCount = tc.CourseDetails.TheoryHoursCount,
                       PraticeHoursCount = tc.CourseDetails.PraticeHoursCount,
                       InternalExam = tc.CourseDetails.InternalExam,
                       CreationDate = tc.CourseDetails.CreationDate,
                       Notes = tc.CourseDetails.Notes
                   }
               }).ToListAsync();

            return Ok(traineeCourses);
        }

        [HttpPost]
        public async Task<ActionResult<TraineeCourseDto>> PostTraineeCourse([FromBody] TraineeCourseDto createTraineeCourseDto)
        {
            //Console.WriteLine($"Otrzymane dane: {JsonConvert.SerializeObject(createTraineeCourseDto)}");  // Zaloguj dane
            if (createTraineeCourseDto == null || createTraineeCourseDto.Client == null)
            {
                return BadRequest("Payload is empty or invalid.");
            }
            var dto = createTraineeCourseDto;
            
            //Console.WriteLine($"ClientId: {dto.Client?.IdClient}, ServiceId: {dto.Service?.IdService}, Quantity: {dto.StartDate}");
            

            try
            {
                // Tworzymy parametr wyjściowy na komunikat
                var resultMessage = new SqlParameter("@resultMessage", SqlDbType.NVarChar, 4000) { Direction = ParameterDirection.Output };

                // Definiujemy parametry wejściowe dla procedury składowanej
                var parameters = new[]
                {
                    new SqlParameter("@IdClient", SqlDbType.Int) { Value = createTraineeCourseDto.Client.IdClient},
                    new SqlParameter("@IdVariantService", SqlDbType.Int) { Value = createTraineeCourseDto.VarinatService.IdVariantService},
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = createTraineeCourseDto.StartDate },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = createTraineeCourseDto.EndDate ?? (object)DBNull.Value},
                    new SqlParameter("@IdStatus", SqlDbType.Int) { Value = createTraineeCourseDto.Status.IdStatus},
                    new SqlParameter("@PESEL", SqlDbType.NVarChar, 11) { Value = createTraineeCourseDto.PESEL},
                    new SqlParameter("@PKK", SqlDbType.NVarChar, 26) { Value = createTraineeCourseDto.PKK},
                    new SqlParameter("@MedicalCheck", SqlDbType.Bit) { Value = createTraineeCourseDto.MedicalCheck},
                    new SqlParameter("@ParentalConsent", SqlDbType.Bit) { Value = createTraineeCourseDto.ParentalConsent ?? (object)DBNull.Value},
                    new SqlParameter("@Notes", SqlDbType.Text) { Value = createTraineeCourseDto.Notes ?? (object)DBNull.Value},
                    new SqlParameter("@PurchaseDate", SqlDbType.DateTime) { Value = createTraineeCourseDto.PurchaseDate},
                    resultMessage
                };

                // Wykonujemy procedurę składowaną
                var execSql = @"
                    EXEC DodajKursantKurs 
                        @IdClient, 
                        @IdVariantService, 
                        @StartDate, 
                        @EndDate, 
                        @IdStatus, 
                        @PESEL, 
                        @PKK, 
                        @MedicalCheck, 
                        @ParentalConsent, 
                        @Notes, 
                        @PurchaseDate, 
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

        [HttpPost("markPresence")]
        public async Task<IActionResult> MarkLecturePresence([FromBody] List<LecturePresence> presenceList)
        {
            if (presenceList == null || !presenceList.Any())
            {
                return BadRequest("Lista obecności jest pusta.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var presence in presenceList)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC AddLecturePresenceAndUpdateHours @IdSzczegoly, @IdHarmonogramWyklad, @DataObecnosci",
                        new SqlParameter("@IdSzczegoly", presence.IdCourseDetails),
                        new SqlParameter("@IdHarmonogramWyklad", presence.IdTheorySchedule),
                        new SqlParameter("@DataObecnosci", presence.PresanceDate)
                    );
                }

                await transaction.CommitAsync();
                return Ok(new { Message = "Obecności zostały zapisane i liczba godzin teorii została zaktualizowana." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { Message = "Wystąpił błąd podczas zapisywania obecności.", Error = ex.Message });
            }
        }

        public class MedicalConsentRequest
        {
            public int IdClient { get; set; }
            public bool MedicalCheck { get; set; }
            public bool? ParentalConsent { get; set; } 
        }


        [HttpPost("updateMedicalAndParentalConsent")]
        public async Task<IActionResult> UpdateMedicalAndParentalConsent([FromBody] List<MedicalConsentRequest> requests)
        {
            if (requests == null || !requests.Any())
            {
                return BadRequest("Brak danych w żądaniu.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach(var request in requests)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC UpdateMedicalAndParentalConsent @IdKlient, @BadaniaLekarskie, @ZgodaRodzica",
                        new SqlParameter("@IdKlient", request.IdClient),
                        new SqlParameter("@BadaniaLekarskie", request.MedicalCheck),
                        new SqlParameter("@ZgodaRodzica", (object?)request.ParentalConsent ?? DBNull.Value) // Obsługa NULL
                    );
                }
                

                await transaction.CommitAsync();
                return Ok(new { Message = "Dane kursanta zostały zaktualizowane." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { Message = "Błąd podczas aktualizacji danych.", Error = ex.Message });
            }
        }


        public class InternalExamRequest
        {
            public List<int> CourseDetailsIds { get; set; }
        }

        [HttpPost("markInternalExam")]
        public async Task<IActionResult> MarkInternalExam([FromBody] InternalExamRequest request)
        {
            if (request == null || request.CourseDetailsIds == null || !request.CourseDetailsIds.Any())
            {
                return BadRequest("Brak kursantów do aktualizacji.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var id in request.CourseDetailsIds)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC UpdateInternalExamStatus @IdSzczegoly",
                        new SqlParameter("@IdSzczegoly", id)
                    );
                }

                await transaction.CommitAsync();
                return Ok(new { Message = "Egzaminy wewnętrzne zostały zaliczone." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { Message = "Błąd podczas aktualizacji egzaminów.", Error = ex.Message });
            }
        }




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


        private bool TraineeCourseExists(int id)
        {
            return _context.TraineeCourses.Any(e => e.IdTraineeCourse == id);
        }
    }
}
