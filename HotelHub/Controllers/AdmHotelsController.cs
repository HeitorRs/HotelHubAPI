using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;

namespace HotelHub.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AdmHotelsController : ControllerBase {
        private readonly TokenService _tokenService;
        private readonly HotelHubContext _context;

        public AdmHotelsController(HotelHubContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/AdmHotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmHotel>>> GetAdmHotel() {
            if (_context.AdmHotel == null) {
                return NotFound();
            }
            return await _context.AdmHotel.ToListAsync();
        }

        // GET: api/AdmHotels/5
        [HttpGet("/Hotels/Adm/{id}")]
        public async Task<ActionResult<List<Hotel>>> GetAdmHotels(int id) {
            if (_context.AdmHotel == null) {
                return NotFound();
            }
            var hotels = await _context.Hotel.Include(h => h.FotosHotel).Where(h => h.Administrador.UserId == id).ToListAsync();

            return hotels;
        }

        //// PUT: api/AdmHotels/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAdmHotel(int id, AdmHotel admHotel) {
        //    if (id != admHotel.AdmHotelId) {
        //        return BadRequest();
        //    }

        //    _context.Entry(admHotel).State = EntityState.Modified;

        //    try {
        //        await _context.SaveChangesAsync();
        //    } catch (DbUpdateConcurrencyException) {
        //        if (!AdmHotelExists(id)) {
        //            return NotFound();
        //        } else {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/AdmHotels
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<AdmHotel>> PostAdmHotel(AdmHotel newadmHotel) {
        //    try {
        //        var admHotel = new AdmHotel {
        //            Nome = newadmHotel.Nome,
        //            Sobrenome = newadmHotel.Sobrenome,
        //            Email = newadmHotel.Email,
        //            Senha = newadmHotel.Senha
        //        };

        //        _context.AdmHotel.Add(admHotel);
        //        await _context.SaveChangesAsync();
        //        var token = _tokenService.GenerateJwtToken(admHotel.AdmHotelId, admHotel.Tipo);
        //        return Ok(token);

        //    } catch (Exception ex) {
        //        return StatusCode(500, $"Erro ao cadastrar hospede: {ex.Message}");
        //    }
        //}

        //// DELETE: api/AdmHotels/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAdmHotel(int id) {
        //    if (_context.AdmHotel == null) {
        //        return NotFound();
        //    }
        //    var admHotel = await _context.AdmHotel.FindAsync(id);
        //    if (admHotel == null) {
        //        return NotFound();
        //    }

        //    _context.AdmHotel.Remove(admHotel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AdmHotelExists(int id) {
        //    return (_context.AdmHotel?.Any(e => e.AdmHotelId == id)).GetValueOrDefault();
        //}
    }
}
