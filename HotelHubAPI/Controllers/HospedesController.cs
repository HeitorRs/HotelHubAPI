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
    public class HospedesController : ControllerBase
    {
        private readonly HotelHubAPIContext _context;

        public HospedesController(HotelHubAPIContext context)
        {
            _context = context;
        }

        // GET: api/Hospedes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospede>>> GetHospede()
        {
          if (_context.Hospede == null)
          {
              return NotFound();
          }
            return await _context.Hospede.ToListAsync();
        }

        // GET: api/Hospedes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospede>> GetHospede(int id)
        {
          if (_context.Hospede == null)
          {
              return NotFound();
          }
            var hospede = await _context.Hospede.FindAsync(id);

            if (hospede == null)
            {
                return NotFound();
            }

            return hospede;
        }

        // PUT: api/Hospedes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospede(int id, Hospede hospede)
        {
            if (id != hospede.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospede).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospedeExists(id))
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

        // POST: api/Hospedes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospede>> PostHospede(Hospede hospede)
        {
          if (_context.Hospede == null)
          {
              return Problem("Entity set 'HotelHubAPIContext.Hospede'  is null.");
          }
            _context.Hospede.Add(hospede);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospede", new { id = hospede.Id }, hospede);
        }

        // DELETE: api/Hospedes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospede(int id)
        {
            if (_context.Hospede == null)
            {
                return NotFound();
            }
            var hospede = await _context.Hospede.FindAsync(id);
            if (hospede == null)
            {
                return NotFound();
            }

            _context.Hospede.Remove(hospede);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HospedeExists(int id)
        {
            return (_context.Hospede?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
