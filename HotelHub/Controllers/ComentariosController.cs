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
    public class ComentariosController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public ComentariosController(HotelHubContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentario()
        {
          if (_context.Comentario == null)
          {
              return NotFound();
          }
            return await _context.Comentario.ToListAsync();
        }

        // GET: api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
          if (_context.Comentario == null)
          {
              return NotFound();
          }
            var comentario = await _context.Comentario.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }

        // PUT: api/Comentarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.ComentarioId)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(int hospedeId, string texto, int quartoId)
        {
            try {
                Hospede hospede = _context.Hospede.Find(hospedeId);
                Quarto quarto = _context.Quarto.Find(quartoId);

                if (hospede == null || quarto == null) {
                    return NotFound("Hospede ou Quarto não encontrado.");
                }
                var comentario = new Comentario {
                    Texto = texto,
                    Hospede = hospede,
                    Quarto = quarto
                };

                _context.Comentario.Add(comentario);
                await _context.SaveChangesAsync();
                return Ok("Comentário criado com sucesso!");

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao criar o comentário: {ex.Message}");
            }
        }

        // DELETE: api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            if (_context.Comentario == null)
            {
                return NotFound();
            }
            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentario.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioExists(int id)
        {
            return (_context.Comentario?.Any(e => e.ComentarioId == id)).GetValueOrDefault();
        }
    }
}
