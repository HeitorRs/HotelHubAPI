using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using System.Drawing;
using FluentAssertions.Equivalency;
using HotelHub.Services;
using Microsoft.AspNetCore.Authorization;

namespace HotelHub.Controllers {
    [Route("api")]
    [ApiController]
    public class QuartosController : ControllerBase {
        private readonly HotelHubContext _context;
        private readonly QuartoService _quartoService;

        public QuartosController(HotelHubContext context, QuartoService quartoService) {
            _context = context;
            _quartoService = quartoService;
        }

        // GET: api/Quartos
        [HttpGet("/quartos/{hotelId}")]
        public async Task<ActionResult<IEnumerable<Quarto>>> GetQuartosDoHotel(int hotelId) {
            List<Quarto> quartosDoHotel =  _context.Quarto.Include(q => q.FotosQuarto).Where(q => q.Hotel.HotelId == hotelId).ToList();

            if (quartosDoHotel == null || quartosDoHotel.Count == 0) {
                return NotFound("Nenhum quarto encontrado para este hotel.");
            }

            return quartosDoHotel;
        }

        // GET: api/Quartos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Quarto>>> GetQuarto(int id) {
            try {
                var quartos = _context.Quarto.Include(q => q.FotosQuarto).Where(q => q.Hotel.HotelId == id).ToList();

                if (quartos == null) {
                    return NotFound("Hotel não encontrado.");
                }

                return quartos;

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar os quartos do hotel: {ex.Message}");
            }
        }
        [HttpGet("quarto/{id}")]
        public async Task<ActionResult<Quarto>> DetalheQuarto(int id) {
            try {
                var quarto = _context.Quarto.Include(q => q.FotosQuarto).FirstOrDefault(q => q.QuartoId == id);

                if (quarto == null) {
                    return NotFound("Quarto não encontrado.");
                }

                return quarto;

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao buscar os quartos do hotel: {ex.Message}");
            }
        }

        // PUT: api/Quartos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuarto(int id, Quarto quarto) {
            if (id != quarto.QuartoId) {
                return BadRequest();
            }

            _context.Entry(quarto).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!QuartoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quartos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/Quarto/Cadastro")]
        public async Task<ActionResult<Quarto>> PostQuarto([FromBody] ModelQuarto model) {
            try {
                await _quartoService.PostQuarto(model.hotelId, model.descricao, model.preco, model.fotos);
                return Ok();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o hotel: {ex.Message}");
            }
        }

        // DELETE: api/Quartos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuarto(int id) {
            if (_context.Quarto == null) {
                return NotFound();
            }
            var quarto = await _context.Quarto.FindAsync(id);
            if (quarto == null) {
                return NotFound();
            }

            _context.Quarto.Remove(quarto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuartoExists(int id) {
            return (_context.Quarto?.Any(e => e.QuartoId == id)).GetValueOrDefault();
        }

        public class ModelQuarto {
            public int hotelId { get; set; }
            public string descricao { get; set; }
            public float preco { get; set; }
            public string fotos { get; set; }
        }
    }
}
