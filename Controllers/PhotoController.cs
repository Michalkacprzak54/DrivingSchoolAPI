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


        [HttpDelete("DeleteImage/{photoId}")]
        public async Task<ActionResult> DeleteImage(int photoId)
        {
            try
            {
                // Znajdź zdjęcie w bazie danych
                var photo = await _context.Photos.FirstOrDefaultAsync(p => p.IdPhoto == photoId);

                if (photo == null)
                {
                    return NotFound("Zdjęcie nie istnieje.");
                }

                // Ścieżka do pliku na serwerze
                string filePath = Path.Combine(_photoDirectory, Path.GetFileName(photo.PhotoPath));

                // Usunięcie pliku z serwera, jeśli istnieje
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Usunięcie zdjęcia z bazy danych
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();

                return Ok("Zdjęcie usunięte pomyślnie.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }

    }

}