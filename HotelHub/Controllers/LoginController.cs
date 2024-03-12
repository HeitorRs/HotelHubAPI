using HotelHub.Data;
using HotelHub.Models;
using HotelHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.Controllers {
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase {

        private readonly TokenService _tokenService;
        private readonly HotelHubContext _context;

        public LoginController(HotelHubContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("/user/login")]
        public IActionResult Login([FromBody] LoginDto model) {
            try {
                var hospede = _context.Hospede.SingleOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);
                if (hospede != null) {
                    var tokenString = _tokenService.GenerateJwtToken(hospede.UserId, hospede.Tipo.ToString()) ;
                    return Ok(new { Token = tokenString });
                }

                // Verifica se o usuário é um administrador de hotel
                var admHotel = _context.AdmHotel.SingleOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);
                if (admHotel != null) {
                    var tokenString = _tokenService.GenerateJwtToken(admHotel.UserId, admHotel.Tipo.ToString());
                    return Ok(new { Token = tokenString });
                }
                return Unauthorized();
            } catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }

        }
        public class LoginDto {
            public string Email { get; set; }
            public string Senha { get; set; }
        }
    }
}
