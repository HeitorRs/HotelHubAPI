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

namespace HotelHub.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase {
        private readonly HotelService _hotelService;
        private readonly HotelHubContext _context;


        public HotelsController(HotelService hotelService, HotelHubContext context) {
            _hotelService = hotelService;
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public IActionResult GetHoteis() {
            try {
                var result = _hotelService.GetAllHotels();
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Hotels/5 [Authorize(Roles ="admHotel")]
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetHotel(int id) {
            try {
                var result = _hotelService.GetHotelPerId(id);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        //// PUT: api/Hotels/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHotel(int id, Hotel hotel) {
        //    if (id != hotel.HotelId) {
        //        return BadRequest();
        //    }

        //    _context.Entry(hotel).State = EntityState.Modified;

        //    try {
        //        await _context.SaveChangesAsync();
        //    } catch (DbUpdateConcurrencyException) {
        //        if (!HotelExists(id)) {
        //            return NotFound();
        //        } else {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "AdmHotel")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(postmodel model) {
            try {
                await _hotelService.PostHotel(model.admHotelId, model.nome, model.descricao, model.cidade, model.fotos);
                return Ok();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro ao cadastrar o hotel: {ex.Message}");
            }
        }

        // DELETE: api/Hotels/5
        [HttpDelete("/api/Hotels/delete/{id}")]
        public async Task<IActionResult> DeleteHotel(int id) {
            if (_context.Hotel == null) {
                return NotFound();
            }
            var result = await _hotelService.DeleteHotel(id);
            if (result == null) {
                return BadRequest();
            };

            return NoContent();
        }
    }
    public class postmodel {
        public int admHotelId { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string cidade { get; set; }
        public string fotos { get; set; }
    }
}
