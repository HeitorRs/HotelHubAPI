using HotelHub.Controllers;
using HotelHub.Data;
using HotelHub.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Services {

    public class HotelService {

        private readonly HotelHubContext _context;

        public HotelService(HotelHubContext context) {
            _context = context;
        }
        public HotelService() { }

        public virtual List<Hotel> GetAllHotels() {
            return _context.Hotel.Include(h => h.FotosHotel).ToList();
        }

        public virtual Hotel GetHotelPerId(int id) {
            return _context.Hotel.Include(h => h.FotosHotel).FirstOrDefault(h => h.HotelId == id);
        }

        public virtual async Task<string> DeleteHotel(int id) {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null) {
                return null; 
            }

            bool existeReserva = await verificarReserva(id);

            if (existeReserva == true) {
                return null;
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return "ok";
        }

        public virtual async Task<Hotel> PostHotel(int admHotel, string nome, string descricao, string cidade, string fotos) {

            AdmHotel admhotel = GetAdmHotel(admHotel);

            List<FotoHotel> fotoshotel = TransformListFotosHotel(fotos);

            var hotel = new Hotel {
                Nome = nome,
                Descricao = descricao,
                Cidade = cidade,
                FotosHotel = fotoshotel,
                Administrador = admhotel
            }
;
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public virtual AdmHotel GetAdmHotel(int admhotelId) {

            AdmHotel admhotel = _context.AdmHotel.Find(admhotelId);
            if (admhotel == null) {
                return null;
            }
            return admhotel;
        }

        public virtual List<FotoHotel> TransformListFotosHotel(string fotos) {

            var fotosList = fotos.Split(',').ToList();

            var fotoshotel = new List<FotoHotel>();
            foreach (string foto in fotosList) {
                var newfoto = new FotoHotel { NomeArquivo = foto };
                fotoshotel.Add(newfoto);
            }
            return fotoshotel;
        }

        public async virtual Task<bool> verificarReserva(int id) {
            List<Reserva> reservas = await _context.Reserva.Include(r => r.Hotel).Where(r => r.Hotel.HotelId == id).ToListAsync();
            if (reservas.Count() > 0) {
                return true;
            }
            return false;
        }
    }
}
