using HotelHub.Data;
using HotelHub.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<string>  DeleteHotel(int id) {
            var hotel = GetHotelPerId(id);

            if (hotel == null) {
                return null;
            }
            
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return "ok";
        }

        public async Task<Hotel> PostHotel(string nome, string descricao, string cidade, string fotos, int admhotelId) {

            AdmHotel admhotel = GetAdmHotel(admhotelId);

            List<FotoHotel> fotoshotel = TransformListFotosHotel(fotos);

            var hotel = new Hotel {
                Nome = nome,
                Descricao = descricao,
                Cidade = cidade,
                FotosHotel = fotoshotel,
                Administrador = admhotel
            };

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
    }
}
