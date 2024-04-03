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
        // GET: api/AdmHotels/5
        [HttpGet("/Hotels/Adm/{id}")]
        public async Task<ActionResult<List<Hotel>>> GetAdmHotels(int id) {
            if (_context.AdmHotel == null) {
                return NotFound();
            }
            var hotels = await _context.Hotel.Include(h => h.FotosHotel).Where(h => h.Administrador.UserId == id).ToListAsync();

            return hotels;
        }
    }
}
