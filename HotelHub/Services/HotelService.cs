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

        public virtual async Task<bool> DeleteHotel(int id) {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null) {
                return false; 
            }

            bool existeReserva = await verificarReserva(id);

            if (existeReserva == true) {
                return false;
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<Hotel> PostHotel(PostModelHotel model) {

            AdmHotel admhotel = GetAdmHotel(model.admHotelId);

            List<FotoHotel> fotoshotel = TransformListFotosHotel(model.fotos);

            var hotel = new Hotel {
                Nome = model.nome,
                Descricao = model.descricao,
                Cidade = model.cidade,
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

        public async Task<bool> PutHotel(PutModelHotel model) {
            try {
                var hotel = _context.Hotel.FirstOrDefault(h => h.HotelId == model.hotelId);
                hotel.Nome = model.nome;
                hotel.Cidade = model.cidade;
                hotel.Descricao = model.descricao;

                _context.Hotel.Update(hotel);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            return true;
        }
        private bool HotelExists(int id) {
            return _context.Hotel.Any(e => e.HotelId == id);
        }

        public class PutModelHotel {
            public int hotelId { get; set; }
            public string nome { get; set; }
            public string cidade { get; set; }
            public string descricao { get; set; }
        }
        public class PostModelHotel {
            public int admHotelId { get; set; }
            public string nome { get; set; }
            public string descricao { get; set; }
            public string cidade { get; set; }
            public string fotos { get; set; }
        }
    }
}
