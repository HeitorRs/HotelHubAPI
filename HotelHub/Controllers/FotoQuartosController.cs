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
    public class FotoQuartosController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public FotoQuartosController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/FotoQuartos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FotoQuarto>>> GetFotoQuarto()
        {
          if (_context.FotoQuarto == null)
          {
              return NotFound();
          }
            return await _context.FotoQuarto.ToListAsync();
        }

        // GET: api/FotoQuartos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FotoQuarto>> GetFotoQuarto(int id)
        {
          if (_context.FotoQuarto == null)
          {
              return NotFound();
          }
            var fotoQuarto = await _context.FotoQuarto.FindAsync(id);

            if (fotoQuarto == null)
            {
                return NotFound();
            }

            return fotoQuarto;
        }

        // PUT: api/FotoQuartos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotoQuarto(int id, FotoQuarto fotoQuarto)
        {
            if (id != fotoQuarto.FotoId)
            {
                return BadRequest();
            }

            _context.Entry(fotoQuarto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoQuartoExists(id))
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

        // POST: api/FotoQuartos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FotoQuarto>> PostFotoQuarto(string nomearquivo, int quartoId)
        {
            try {
                Quarto quarto = _context.Quarto.Include(h => h.FotosQuarto).FirstOrDefault(h => h.QuartoId == quartoId);

                if (quarto == null) {
                    return NotFound("Quarto não encontrado.");
                }
                var fotosquarto = quarto.FotosQuarto;
                var newfoto = new FotoQuarto { NomeArquivo = nomearquivo };
                fotosquarto.Add(newfoto);
                await _context.SaveChangesAsync();
                return Ok("Foto adicionada com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao adiconar foto ao hotel: {ex.Message}");
            }
        }

        // DELETE: api/FotoQuartos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotoQuarto(int id)
        {
            if (_context.FotoQuarto == null)
            {
                return NotFound();
            }
            var fotoQuarto = await _context.FotoQuarto.FindAsync(id);
            if (fotoQuarto == null)
            {
                return NotFound();
            }

            _context.FotoQuarto.Remove(fotoQuarto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotoQuartoExists(int id)
        {
            return (_context.FotoQuarto?.Any(e => e.FotoId == id)).GetValueOrDefault();
        }
    }
}
