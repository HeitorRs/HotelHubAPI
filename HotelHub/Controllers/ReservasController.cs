using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using System.Globalization;

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
        [HttpGet("/reservas/{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id) {
            try {
                Reserva  reserva =  _context.Reserva.Include(r => r.Hotel).ThenInclude(r => r.Quartos).FirstOrDefault(r => r.ReservaId == id);
                return reserva;

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Reservas/5
        [HttpGet("/reservas/hospede/{id}")]
        public async Task<ActionResult<List<Reserva>>> GetReservasHospede(int id)
        {
            try {
                List<Reserva> reservas = await _context.Reserva.Include(r => r.Hotel).ThenInclude(r => r.Quartos).Where(r => r.Hospede.UserId == id).ToListAsync();
                return reservas;

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

        [HttpGet("/reservas/quarto/{quartoId}")]
        public async Task<ActionResult<List<ReservaDataModel>>> GetReservasPorQuarto(int quartoId) {
            var datasreservadas = new List<ReservaDataModel>();
            try {
                var reservas = await _context.Reserva.Where(r => r.Quarto.QuartoId == quartoId).ToListAsync();
                foreach(var reserva in reservas) {
                    var datareservada = new ReservaDataModel {
                        DataEntrada = reserva.DataEntrada.ToString("yyyy-MM-dd"),
                        DataSaida = reserva.DataSaida.ToString("yyyy-MM-dd")
                    };
                    datasreservadas.Add(datareservada);
                }
                return Ok(datasreservadas);
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar reservas: {ex.Message}");
            }
        }

        public class ReservaDataModel {
            public string DataEntrada { get; set; }
            public string DataSaida { get; set; }
        }

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/Reservar")]
        public async Task<ActionResult<Reserva>> PostReserva([FromBody] ReservaModel model)
        {
            try {
                DateTime dataEntrada = DateTime.ParseExact(model.dataentrada, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime dataSaida = DateTime.ParseExact(model.datasaida, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                Hospede hospede = _context.Hospede.Find(model.hospedeid);
                Quarto quarto = _context.Quarto.Find(model.quartoid);
                Hotel hotel = _context.Hotel.Find(model.hotelid);

                if (hotel == null) {
                    return NotFound("Hotel não encontrado.");
                } else if (quarto == null) {
                    return NotFound("Quarto não encontrado.");
                } else if (hospede == null) {
                    return NotFound("Hospede não encontrado");
                }

                var reserva = new Reserva {
                    DataEntrada = dataEntrada,
                    DataSaida = dataSaida,
                    Observacao = model.observacao,
                    Hotel = hotel,
                    Quarto = quarto,
                    Hospede = hospede
                };

                reserva.CalcularValorTotal();

                _context.Reserva.Add(reserva);
                await _context.SaveChangesAsync();

                return Ok("Reserva feita com sucesso!");
            } catch (Exception ex) {
                return StatusCode(500, $"Erro fazer reserva: {ex.Message}");
            }
        }

        // DELETE: api/Reservas/5
        [HttpDelete("/Reserva/Delete/{id}")]
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

        public class ReservaModel {
            public string dataentrada { get; set; }
            public string datasaida { get; set; }
            public string observacao { get; set; }
            public int hotelid { get; set; }
            public int quartoid { get; set; }
            public int hospedeid { get; set; }
        }
    }
}
