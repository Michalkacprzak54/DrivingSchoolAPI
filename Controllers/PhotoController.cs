using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly string _photoDirectory = @"C:\Users\micha\source\repos\DrivingSchoolReactApp\drivingschoolreactapp.client\public\images";
        public PhotoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Photo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await _context.Photos.ToListAsync();
            

        }

        // GET: api/Photo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstAsync(p => p.IdPhoto == id);

            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage([FromForm] int serviceId, [FromForm] string alternativeDescription)
        {
            try
            {
                var uploadedFiles = Request.Form.Files;

                if (uploadedFiles.Count == 0)
                    return BadRequest("Nie przesłano żadnego pliku.");

                foreach (IFormFile source in uploadedFiles)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(source.FileName); // Unikalna nazwa pliku
                    string imagePath = Path.Combine(_photoDirectory, filename);

                    // Upewniamy się, że katalog istnieje
                    if (!Directory.Exists(_photoDirectory))
                    {
                        Directory.CreateDirectory(_photoDirectory);
                    }

                    // Zapis pliku na serwerze
                    using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await source.CopyToAsync(stream);
                    }

                    // Zapis ścieżki do bazy danych
                    var photo = new Photo
                    {
                        IdService = serviceId,
                        PhotoPath = $"images/{filename}", 
                        AlternativeDescription = alternativeDescription
                    };

                    _context.Photos.Add(photo);
                }

                await _context.SaveChangesAsync();

                return Ok("Zdjęcie zostało dodane i zapisane w bazie danych.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }





        // DELETE: api/Photo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}