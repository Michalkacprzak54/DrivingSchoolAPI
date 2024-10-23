using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Data;
using DrivingSchoolAPI.Entities;

namespace DrivingSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {
        private readonly DataContext _context;

        public ZipCodeController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ZipCode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipCode()
        {
            return await _context.ZipCodes.ToListAsync();
        }

        // GET: api/ZipCode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZipCode>> GetZipCode(int id)
        {
            var zipCode = await _context.ZipCodes.FindAsync(id);

            if (zipCode == null)
            {
                return NotFound();
            }

            return zipCode;
        }
        // POST: api/ZipCode
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ZipCode>> PostZipCode(ZipCode zipCode)
        {
            _context.ZipCodes.Add(zipCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZipCode", new { id = zipCode.IdZipCode }, zipCode);
        }

        // PUT: api/ZipCode/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutZipCode(int id, ZipCode zipCode)
        //{
        //    if (id != zipCode.IdZipCode)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(zipCode).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ZipCodeExists(id))
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



        // DELETE: api/ZipCode/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteZipCode(int id)
        //{
        //    var zipCode = await _context.ZipCode.FindAsync(id);
        //    if (zipCode == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ZipCode.Remove(zipCode);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ZipCodeExists(int id)
        //{
        //    return _context.ZipCode.Any(e => e.IdZipCode == id);
        //}
    }
}
