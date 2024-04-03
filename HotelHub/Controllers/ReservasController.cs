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
using HotelHub.Services;
using static HotelHub.Services.ReservaService;

namespace HotelHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService) {
            _reservaService = reservaService;
        }
            
        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva() {
            var result = await _reservaService.GetReservas();
            if (result == null){
                return NotFound();
              }
            return result;
        }

        // GET
        [HttpGet("/reservas/{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id) {
            var  reserva = await _reservaService.GetReserva(id);
            if (reserva == null) {
                return BadRequest();
            }
            return Ok(reserva);
        }

        // GET
        [HttpGet("/reservas/hospede/{userId}")]
        public async Task<ActionResult<List<Reserva>>> GetReservasHospede(int userId) {
            var result = await _reservaService.GetReservasHospede(userId);
            if (result == null) {
                return BadRequest();
            }
            return Ok(result);
        }
        // GET
        [HttpGet("/reservas/quarto/{quartoId}")]
        public async Task<ActionResult<List<ReservaDataModel>>> GetReservasPorQuarto(int quartoId) {
            var result = await _reservaService.GetReservasPorQuarto(quartoId);
            if (result == null) {
                return BadRequest();
            }
            return Ok(result);
        }

        // POST
        [HttpPost("/Reservar")]
        public async Task<ActionResult<Reserva>> PostReserva([FromBody] ReservaModel model) {
           var result = await _reservaService.PostReserva(model);
            if(result == false) {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/Reservas/5
        [HttpDelete("/Reserva/Delete/{id}")]
        public async Task<IActionResult> DeleteReserva(int id) {
            var result = await _reservaService.DeleteReservas(id);
            if(result == false) { 
                return BadRequest(); 
            }
            return NoContent();
        }
    }
}
