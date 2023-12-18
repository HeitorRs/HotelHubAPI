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
    public class ReservasController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public ReservasController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva()
        {
          if (_context.Reserva == null)
          {
              return NotFound();
          }
            return await _context.Reserva.ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            try {
                Reserva reserva = await _context.Reserva.Include(r => r.Hotel).ThenInclude(r => r.Quartos).Include(r => r.Hospede).FirstOrDefaultAsync(r => r.ReservaId == id);
                return reserva;

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.ReservaId)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(DateTime dataentrada, DateTime datasaida, string observacao, int quartoId, int hotelId, int hospedeId)
        {
            try {
                Hospede hospede = _context.Hospede.Find(hospedeId);
                Quarto quarto = _context.Quarto.Find(hospedeId);
                Hotel hotel = _context.Hotel.Find(hotelId);

                if (hotel == null ){
                    return NotFound("Hotel não encontrado.");
                }else if(quarto == null) {
                    return NotFound("Quarto não encontrado."); 
                }else if(hospede == null) {
                    return NotFound("Hospede não encontrado");
                }
                var reserva = new Reserva {
                    DataEntrada = dataentrada,
                    DataSaida = datasaida,
                    Observacao = observacao,
                    Hotel = hotel,
                    Quarto = quarto,
                    Hospede= hospede
                };

                _context.Reserva.Add(reserva);
                await _context.SaveChangesAsync();
                return Ok("Reserva feita com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro fazer reserva: {ex.Message}");
            }
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            if (_context.Reserva == null)
            {
                return NotFound();
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return (_context.Reserva?.Any(e => e.ReservaId == id)).GetValueOrDefault();
        }
    }
}
