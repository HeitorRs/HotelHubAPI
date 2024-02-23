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

namespace HotelHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuartosController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public QuartosController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/Quartos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quarto>>> GetQuarto()
        {
          if (_context.Quarto == null)
          {
              return NotFound();
          }
            return await _context.Quarto.ToListAsync();
        }

        // GET: api/Quartos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Quarto>>> GetQuarto(int id)
        {
            try {
                var quartos = _context.Quarto.Include(q => q.FotosQuarto).Where(q => q.Hotel.HotelId== id).ToList(); 

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
        public async Task<IActionResult> PutQuarto(int id, Quarto quarto)
        {
            if (id != quarto.QuartoId)
            {
                return BadRequest();
            }

            _context.Entry(quarto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuartoExists(id))
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

        // POST: api/Quartos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quarto>> PostQuarto(List<string> fotos, string descricao, float preco, int hotelId)
        {
            try {
                Hotel hotel = _context.Hotel.Find(hotelId);

                if (hotel == null) {
                    return NotFound("Hotel não encontrado.");
                }
                var fotosquarto = new List<FotoQuarto> { };
                foreach (string foto in fotos) {
                    var newfoto = new FotoQuarto { NomeArquivo = foto };
                    fotosquarto.Add(newfoto);
                }
                var quarto = new Quarto {
                    FotosQuarto = fotosquarto,
                    Descricao = descricao,
                    Preco = preco,
                    Hotel = hotel
                };

                _context.Quarto.Add(quarto);
                await _context.SaveChangesAsync();
                return Ok("Quarto cadastrado com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o quarto: {ex.Message}");
            }
        }

        // DELETE: api/Quartos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuarto(int id)
        {
            if (_context.Quarto == null)
            {
                return NotFound();
            }
            var quarto = await _context.Quarto.FindAsync(id);
            if (quarto == null)
            {
                return NotFound();
            }

            _context.Quarto.Remove(quarto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuartoExists(int id)
        {
            return (_context.Quarto?.Any(e => e.QuartoId == id)).GetValueOrDefault();
        }
    }
}
