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
using Microsoft.AspNetCore.Authorization;
using static HotelHub.Services.HotelService;

namespace HotelHub.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase {
        private readonly HotelService _hotelService;


        public HotelsController(HotelService hotelService) {
            _hotelService = hotelService;
        }

        // GET
        [HttpGet]
        public IActionResult GetHoteis() {
            try {
                var result = _hotelService.GetAllHotels();
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET por id
        [HttpGet("{id}")]
        public IActionResult GetHotel(int id) {
            try {
                var result = _hotelService.GetHotelPerId(id);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "AdmHotel")]
        public async Task<IActionResult> PutHotel(PutModelHotel model) {

            var result = await _hotelService.PutHotel(model);
            if (result == true) {
                return Ok();
            }
            return BadRequest();
        }

        // POST
        [Authorize(Roles = "AdmHotel")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(PostModelHotel model) {
            try {
                await _hotelService.PostHotel(model);
                return Ok();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o hotel: {ex.Message}");
            }
        }

        // DELETE
        [Authorize(Roles = "AdmHotel")]
        [HttpDelete("/api/Hotels/delete/{id}")]
        public async Task<IActionResult> DeleteHotel(int id) {
            var result = await _hotelService.DeleteHotel(id);
            if (result == false) {
                return BadRequest();
            };
            return NoContent();
        }
    } 
}
