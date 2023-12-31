﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using Microsoft.AspNetCore.Authorization;

namespace HotelHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospedesController : ControllerBase
    {
        private readonly HotelHubContext _context;

        public HospedesController(HotelHubContext context)
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
            if (id != hospede.HospedeId)
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
        public async Task<ActionResult<Hospede>> PostHospede(Hospede newhospede)
        {
            try {
                var hospede = new Hospede {
                    Nome = newhospede.Nome,
                    Sobrenome = newhospede.Sobrenome,
                    Email = newhospede.Email, 
                    Senha = newhospede.Senha
                };

                _context.Hospede.Add(hospede);
                await _context.SaveChangesAsync();
                var token = TokenService.GenerateToken(hospede);
                return Ok(token);

            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar hospede: {ex.Message}");
            }
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
            return (_context.Hospede?.Any(e => e.HospedeId == id)).GetValueOrDefault();
        }
    }
}
