using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;

namespace HotelHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoHotelsController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public FotoHotelsController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/FotoHotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FotoHotel>>> GetFotoHotel()
        {
          if (_context.FotoHotel == null)
          {
              return NotFound();
          }
            return await _context.FotoHotel.ToListAsync();
        }

        // GET: api/FotoHotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FotoHotel>> GetFotoHotel(int id)
        {
          if (_context.FotoHotel == null)
          {
              return NotFound();
          }
            var fotoHotel = await _context.FotoHotel.FindAsync(id);

            if (fotoHotel == null)
            {
                return NotFound();
            }

            return fotoHotel;
        }

        // PUT: api/FotoHotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotoHotel(int id, FotoHotel fotoHotel)
        {
            if (id != fotoHotel.FotoId)
            {
                return BadRequest();
            }

            _context.Entry(fotoHotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoHotelExists(id))
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

        // POST: api/FotoHotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FotoHotel>> PostFotoHotel(string nomearquivo, int hotelId)
        {
            try {
                Hotel hotel = _context.Hotel.Include(h => h.FotosHotel).FirstOrDefault(h => h.HotelId == hotelId);

                if (hotel == null) {
                    return NotFound("Hotel não encontrado.");
                }
                var fotoshotel = hotel.FotosHotel;
                var newfoto = new FotoHotel { NomeArquivo = nomearquivo};
                fotoshotel.Add(newfoto);
                await _context.SaveChangesAsync();
                return Ok("Foto adicionada com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao adiconar foto ao hotel: {ex.Message}");
            }
        }

        // DELETE: api/FotoHotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotoHotel(int id)
        {
            if (_context.FotoHotel == null)
            {
                return NotFound();
            }
            var fotoHotel = await _context.FotoHotel.FindAsync(id);
            if (fotoHotel == null)
            {
                return NotFound();
            }

            _context.FotoHotel.Remove(fotoHotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotoHotelExists(int id)
        {
            return (_context.FotoHotel?.Any(e => e.FotoId == id)).GetValueOrDefault();
        }
    }
}
