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
    public class QuartosController : ControllerBase
    {
        private readonly HotelHubAPIContext _context;

        public QuartosController(HotelHubAPIContext context)
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
        public async Task<ActionResult<Quarto>> GetQuarto(int id)
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

            return quarto;
        }

        // PUT: api/Quartos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuarto(int id, Quarto quarto)
        {
            if (id != quarto.Id)
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
        public async Task<ActionResult<Quarto>> PostQuarto(Quarto quarto)
        {
          if (_context.Quarto == null)
          {
              return Problem("Entity set 'HotelHubAPIContext.Quarto'  is null.");
          }
            _context.Quarto.Add(quarto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuarto", new { id = quarto.Id }, quarto);
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
            return (_context.Quarto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
