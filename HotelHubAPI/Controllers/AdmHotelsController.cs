using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHubAPI.Data;
using HotelHubAPI.Models;

namespace HotelHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmHotelsController : ControllerBase
    {
        private readonly HotelHubAPIContext _context;

        public AdmHotelsController(HotelHubAPIContext context)
        {
            _context = context;
        }

        // GET: api/AdmHotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmHotel>>> GetAdmHotel()
        {
          if (_context.AdmHotel == null)
          {
              return NotFound();
          }
            return await _context.AdmHotel.ToListAsync();
        }

        // GET: api/AdmHotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmHotel>> GetAdmHotel(int id)
        {
          if (_context.AdmHotel == null)
          {
              return NotFound();
          }
            var admHotel = await _context.AdmHotel.FindAsync(id);

            if (admHotel == null)
            {
                return NotFound();
            }

            return admHotel;
        }

        // PUT: api/AdmHotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmHotel(int id, AdmHotel admHotel)
        {
            if (id != admHotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(admHotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmHotelExists(id))
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

        // POST: api/AdmHotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdmHotel>> PostAdmHotel(AdmHotel admHotel)
        {
          if (_context.AdmHotel == null)
          {
              return Problem("Entity set 'HotelHubAPIContext.AdmHotel'  is null.");
          }
            _context.AdmHotel.Add(admHotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmHotel", new { id = admHotel.Id }, admHotel);
        }

        // DELETE: api/AdmHotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmHotel(int id)
        {
            if (_context.AdmHotel == null)
            {
                return NotFound();
            }
            var admHotel = await _context.AdmHotel.FindAsync(id);
            if (admHotel == null)
            {
                return NotFound();
            }

            _context.AdmHotel.Remove(admHotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdmHotelExists(int id)
        {
            return (_context.AdmHotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
