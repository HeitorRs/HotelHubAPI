using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using static HotelHub.Services.TokenService;
using System.Threading.Tasks.Sources;
using Microsoft.AspNetCore.Http.Features;

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

        // PUT: api/Hospedes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/user/edit")]
        public async Task<IActionResult> PutHospede(EditModel model) {

            try {
                if (model.Tipo == "AdmHotel") {
                    var admhotel = _context.AdmHotel.Find(model.id);
                    if(model.Nome != "") {
                        admhotel.Nome = model.Nome;
                    }
                    if (model.Sobrenome != "") {
                        admhotel.Sobrenome = model.Sobrenome;
                    }
                    if (model.Email != "") {
                        admhotel.Email = model.Email;
                    }
                    _context.AdmHotel.Update(admhotel);
                    await _context.SaveChangesAsync();

                } else if (model.Tipo == "Hospede") {
                    var hospede = _context.Hospede.Find(model.id);
                    if (model.Nome != "") {
                        hospede.Nome = model.Nome;
                    }
                    if (model.Sobrenome != "") {
                        hospede.Sobrenome = model.Sobrenome;
                    }
                    if (model.Email != "") {
                        hospede.Email = model.Email;
                    }

                    _context.Hospede.Update(hospede);
                    await _context.SaveChangesAsync();
                }
                return Ok();

                } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

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

        // DELETE: api/Hospedes/5
        [HttpDelete("/user/delete/{id}/{tipo}")]
        public async Task<IActionResult> DeleteUser(int id, string tipo) {
            try {
                if (tipo == "Hospede") {
                    var hospede = await _context.Hospede.FindAsync(id);
                    if (hospede == null) {
                        return NotFound();
                    }
                    _context.Hospede.Remove(hospede);
                    await _context.SaveChangesAsync();
                    return NoContent();
                } else if (tipo == "AdmHotel") {
                    var admhotel = await _context.AdmHotel.Include(a => a.HoteisGerenciados).ThenInclude(h => h.Reservas).FirstOrDefaultAsync(a => a.UserId == id);
                    if (admhotel == null) {
                        return NotFound();
                    }
                    var reservas = admhotel.HoteisGerenciados.SelectMany(hotel => hotel.Reservas).ToList();
                    if (reservas.Count > 0) {
                        return BadRequest("O usuário possui reservas associadas e não pode ser excluído.");
                    }
                    _context.AdmHotel.Remove(admhotel);
                    await _context.SaveChangesAsync();
                    return NoContent();
                } else {
                    return BadRequest("Tipo de usuário inválido.");
                }
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao excluir usuário: {ex.Message}");
            }
        }
     }
    public class CadastroModel {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public TipoUsuario Tipo { get; set; }
    }

    public class EditModel {
        public int id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Tipo { get; set; }
    }

    public class DeleteModel {
        public int id { get; set; }
        public string Tipo { get; set; }
    }
}
