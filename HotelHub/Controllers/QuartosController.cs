using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHub.Data;
using HotelHub.Models;
using System.Drawing;
using FluentAssertions.Equivalency;
using HotelHub.Services;
using Microsoft.AspNetCore.Authorization;
using static HotelHub.Services.QuartoService;

namespace HotelHub.Controllers {
    [Route("api")]
    [ApiController]
    public class QuartosController : ControllerBase {
        private readonly QuartoService _quartoService;

        public QuartosController(QuartoService quartoService) {
            _quartoService = quartoService;
        }

        // GET
        [HttpGet("/quartos/{hotelId}")]
        public async Task<ActionResult<IEnumerable<Quarto>>> GetQuartosDoHotel(int hotelId) {
            var result = await _quartoService.GetQuartos(hotelId);

            if (result == null) {
                return NotFound("Nenhum quarto encontrado para este hotel.");
            }
            return Ok(result);
        }

        // GET quarto
        [HttpGet("/quartos/detalhes/{id}")]
        public async Task<ActionResult<Quarto>> GetQuarto(int id) {
            var result = await _quartoService.GetQuarto(id);
            if(result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        // GET detalhes do quarto
        [HttpGet("quarto/{id}")]
        public async Task<ActionResult<Quarto>> DetalheQuarto(int id) {
            var result = await _quartoService.GetDetalheQuarto(id);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        // PUT
        [HttpPut("/quarto/edit/{id}")]
        public async Task<IActionResult> PutQuarto(PutModelQuarto model) {
            var result = await _quartoService.PutQuarto(model);
            if (result == false) {
                return NotFound();
            }
            return Ok(result);
        }

        // POST
        [HttpPost("/Quarto/Cadastro")]
        public async Task<ActionResult<Quarto>> PostQuarto([FromBody] ModelQuarto model) {
            try {
                await _quartoService.PostQuarto(model);
                return Ok();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o hotel: {ex.Message}");
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuarto(int id) {
            var result = await _quartoService.DeleteQuarto(id);
            if(result == false) {
                return StatusCode(500, $"Erro ao deletar o hotel");
            }
            return NoContent();
        }
    }
}
