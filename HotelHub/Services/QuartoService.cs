using HotelHub.Data;
using HotelHub.Models;

namespace HotelHub.Services {
    public class QuartoService {

        private readonly HotelHubContext _context;

        public QuartoService(HotelHubContext context) {
            _context = context;
        }
        public QuartoService() { }

        public async Task<Quarto> PostQuarto(int hotelId, string descricao, float preco, string fotos) {

            Hotel hotel = _context.Hotel.Find(hotelId);

            List<FotoQuarto> fotosquarto = TransformListFotosQuarto(fotos);

            var quarto = new Quarto {
                FotosQuarto = fotosquarto,
                Descricao = descricao,
                Preco = preco,
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
    }
}
