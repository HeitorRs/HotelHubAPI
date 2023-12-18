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
    public class HotelsController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public HotelsController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            return await _context.Hotel.Include(h => h.FotosHotel).ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            var hotel = _context.Hotel.Include(h => h.FotosHotel).FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(string nome, string descricao, string cidade, List<string> fotos, int admhotelId)
        {
            try {
                AdmHotel admhotel = _context.AdmHotel.Find(admhotelId);

                if (admhotel == null) {
                    return NotFound("Administrador não encontrado.");
                }
                var fotoshotel = new List<FotoHotel> {};
                foreach (string foto in fotos) {
                    var newfoto = new FotoHotel { NomeArquivo = foto };
                    fotoshotel.Add(newfoto);
                }

                var hotel = new Hotel {
                    Nome = nome,
                    Descricao = descricao,
                    Cidade = cidade,
                    FotosHotel = fotoshotel,
                    Administrador = admhotel
                };

                _context.Hotel.Add(hotel);
                await _context.SaveChangesAsync();
                return Ok("Hotel cadastrado com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o hotel: {ex.Message}");
            }
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.HotelId == id)).GetValueOrDefault();
        }
    }
}
