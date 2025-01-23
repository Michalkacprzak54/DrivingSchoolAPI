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


        private bool TraineeCourseExists(int id)
        {
            return _context.TraineeCourses.Any(e => e.IdTraineeCourse == id);
        }
    }
}
