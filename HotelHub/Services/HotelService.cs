using HotelHub.Data;
using HotelHub.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Services {

    public class HotelService {

        private readonly HotelHubContext _context;

        public HotelService(HotelHubContext context) {
            _context = context;
        }
        public HotelService() {}

        public virtual List<Hotel> GetAllHotels() {
            return _context.Hotel.Include(h => h.FotosHotel).ToList();
        }

        public virtual Hotel GetHotelPerId(int id) {
            return _context.Hotel.Include(h => h.FotosHotel).FirstOrDefault(h => h.HotelId == id);
        }
    }
}
