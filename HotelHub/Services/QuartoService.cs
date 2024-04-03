using HotelHub.Data;
using HotelHub.Models;
using Microsoft.EntityFrameworkCore;
using static HotelHub.Controllers.QuartosController;

namespace HotelHub.Services {
    public class QuartoService {

        private readonly HotelHubContext _context;

        public QuartoService(HotelHubContext context) {
            _context = context;
        }
        public QuartoService() { }


        public async Task<List<Quarto>> GetQuartos(int hotelId) {
            List<Quarto> quartosDoHotel = _context.Quarto.Include(q => q.FotosQuarto).Where(q => q.Hotel.HotelId == hotelId).ToList();

            if (quartosDoHotel == null || quartosDoHotel.Count == 0) {
                return null;
            }
            return quartosDoHotel;
        }

        public async Task<Quarto> GetQuarto(int id) {
            try {
                var quarto = _context.Quarto.Include(q => q.FotosQuarto).FirstOrDefault(q => q.QuartoId == id);

                if (quarto == null) {
                    return null;
                }
                return quarto;
            } catch (Exception ex) {
                return null;
            }
        }

        public async Task<Quarto> GetDetalheQuarto(int id) {
            try {
                var quarto = _context.Quarto.Include(q => q.FotosQuarto).FirstOrDefault(q => q.QuartoId == id);

                if (quarto == null) {
                    return null;
                }
                return quarto;

            } catch (Exception ex) {
                return null;
            }
        }


        public async Task<Quarto> PostQuarto(ModelQuarto model) {

            Hotel hotel = _context.Hotel.Find(model.hotelId);

            List<FotoQuarto> fotosquarto = TransformListFotosQuarto(model.fotos);

            var quarto = new Quarto {
                FotosQuarto = fotosquarto,
                Descricao = model.descricao,
                Preco = model.preco,
                Hotel = hotel
            };

            _context.Quarto.Add(quarto);
            await _context.SaveChangesAsync();

            return quarto;
        }
        public virtual List<FotoQuarto> TransformListFotosQuarto(string fotos) {

            var fotosList = fotos.Split(',').ToList();

            var fotosquarto = new List<FotoQuarto>();
            foreach (string foto in fotosList) {
                var newfoto = new FotoQuarto { NomeArquivo = foto };
                fotosquarto.Add(newfoto);
            }
            return fotosquarto;
        }

        public async Task<bool> PutQuarto(PutModelQuarto model) {
            try {
                var quarto = _context.Quarto.FirstOrDefault(q => q.QuartoId == model.quartoId);
                quarto.Descricao = model.descricao;
                quarto.Preco = model.preco;

                _context.Quarto.Update(quarto);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteQuarto(int id) {
            if (_context.Quarto == null) {
                return false;
            }
            var quarto = await _context.Quarto.FindAsync(id);
            if (quarto == null) {
                return false;
            }

            _context.Quarto.Remove(quarto);
            await _context.SaveChangesAsync();

            return true;
        }

        public class PutModelQuarto {
            public int quartoId { get; set; }
            public string descricao { get; set; }
            public float preco { get; set; }
        }

        public class ModelQuarto {
            public int hotelId { get; set; }
            public string descricao { get; set; }
            public float preco { get; set; }
            public string fotos { get; set; }
        }
    }
}
