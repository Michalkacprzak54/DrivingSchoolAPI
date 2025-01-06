using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;


namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public InstructorDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDetails>>> GetInstructorsDetails()
        {
            return await _context.InstructorDetails
                .Include(id => id.Instructor)
                .Include(id => id.City)
                .Include(id => id.ZipCode)
                .ToListAsync();
        }

        // Pobieranie szczegółów instruktora na podstawie jego ID
        [HttpGet("{idInstructor}")]
        public async Task<ActionResult<InstructorDetails>> GetInstructorDetail(int idInstructor)
        {
            var instructorDetail = await _context.InstructorDetails
                .Include(id => id.Instructor)
                .Include(id => id.City)
                .Include(id => id.ZipCode)
                .FirstOrDefaultAsync(id => id.IdInstructor == idInstructor);

            if (instructorDetail == null)
            {
                return NotFound();  // Jeśli nie znaleziono, zwróć 404 Not Found
            }

            return Ok(instructorDetail);  // Zwróć szczegóły instruktora
        }

    }
}
