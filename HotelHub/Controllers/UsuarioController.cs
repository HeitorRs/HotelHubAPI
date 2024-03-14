using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using static HotelHub.Services.TokenService;

namespace HotelHub.Controllers {
    [Route("api")]
    [ApiController]
    public class UsuarioController : ControllerBase {

        private readonly TokenService _tokenService;
        private readonly HotelHubContext _context;

        public UsuarioController(HotelHubContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        //// GET: api/Hospedes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Hospede>>> GetHospede() {
        //    if (_context.Hospede == null) {
        //        return NotFound();
        //    }
        //    return await _context.Hospede.ToListAsync();
        //}

        // GET: api/Hospedes/5
        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUserDetails(int id) {

            var hospede = await _context.Hospede.FindAsync(id);
            if (hospede != null) {
 
                return Ok(hospede);
            }

            var admHotel = await _context.AdmHotel.FindAsync(id);
            if (admHotel != null) {
                return Ok(admHotel);
            }

            return NotFound();
        }

        //// PUT: api/Hospedes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHospede(int id, Hospede hospede) {
        //    if (id != hospede.HospedeId) {
        //        return BadRequest();
        //    }

        //    _context.Entry(hospede).State = EntityState.Modified;

        //    try {
        //        await _context.SaveChangesAsync();
        //    } catch (DbUpdateConcurrencyException) {
        //        if (!HospedeExists(id)) {
        //            return NotFound();
        //        } else {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Hospedes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/user/cadastro")]
        public async Task<ActionResult> PostHospede([FromBody] CadastroModel model) {

            try {
                if (model.Tipo.ToString() == "Hospede") {
                    var hospede = new Hospede {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Email = model.Email,
                        Senha = model.Senha,
                        Tipo = model.Tipo
                    };

                    _context.Hospede.Add(hospede);
                    await _context.SaveChangesAsync();
                    var token = _tokenService.GenerateJwtToken(hospede.UserId, hospede.Tipo.ToString());
                    return Ok(new { Token = token });

                } else if (model.Tipo.ToString() == "AdmHotel") {
                    var admhotel = new AdmHotel {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Email = model.Email,
                        Senha = model.Senha,
                        Tipo = model.Tipo
                    };
                    _context.AdmHotel.Add(admhotel);
                    await _context.SaveChangesAsync();
                    var token = _tokenService.GenerateJwtToken(admhotel.UserId, admhotel.Tipo.ToString());
                    return Ok(new { Token = token });
                }
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar hospede: {ex.Message}");
            }
            return BadRequest();
        }

        //// DELETE: api/Hospedes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHospede(int id) {
        //    if (_context.Hospede == null) {
        //        return NotFound();
        //    }
        //    var hospede = await _context.Hospede.FindAsync(id);
        //    if (hospede == null) {
        //        return NotFound();
        //    }

        //    _context.Hospede.Remove(hospede);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool HospedeExists(int id) {
        //    return (_context.Hospede?.Any(e => e.HospedeId == id)).GetValueOrDefault();
        //}
    }
    public class CadastroModel {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public TipoUsuario Tipo { get; set; }
    }
}
