using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using static HotelHub.Services.TokenService;
using System.Threading.Tasks.Sources;
using Microsoft.AspNetCore.Http.Features;
using static HotelHub.Services.UsuarioService;

namespace HotelHub.Controllers {
    [Route("api")]
    [ApiController]
    public class UsuarioController : ControllerBase {

        private readonly UsuarioService _usuarioservice;

        public UsuarioController(UsuarioService usuarioservice) {
            _usuarioservice = usuarioservice;
        }

        // GET
        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUserDetails(int id) {

            var result = await _usuarioservice.GetUsuarioDetails(id);

            if (result != null) {
                return Ok(result);
            }
            return NotFound();
        }

        // PUT
        [HttpPut("/user/edit")]
        public async Task<IActionResult> PutUser(EditModel model) {

            var result = await _usuarioservice.PutUsuario(model);
            if (result == true) {
                return Ok();
            }
            return BadRequest();
        }

        // POST
        [HttpPost("/user/cadastro")]
        public async Task<ActionResult> PostUser([FromBody] CadastroModel model) {

             var result = await _usuarioservice.PostUsuario(model);
            if(result != null) {
                return Ok(new { Token = result });
            }
            return BadRequest();
        }

        // DELETE
        [HttpDelete("/user/delete/{id}/{tipo}")]
        public async Task<IActionResult> DeleteUser(int id, string tipo) {
            var result = await _usuarioservice.DeleteUsuario(id, tipo);
            if (result == true) {
                return NoContent();
            }
            return StatusCode(500, $"Erro ao excluir usuário");
        }
     }
}
