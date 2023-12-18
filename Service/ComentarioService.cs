using HotelHub.Data;
using HotelHub.Models;


namespace Service {
    public class ComentarioService {

        private readonly HotelHubContext dbContext;

        public ComentarioService(HotelHubContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task CriarComentario(string texto, int hospedeId, int quartoId) {

            Hospede hospede = dbContext.Hospede.Find(hospedeId);
            Quarto quarto = dbContext.Quarto.Find(quartoId);

            var comentario = new Comentario {
                Texto = texto,
                Hospede = hospede,
                Quarto = quarto
            };

            dbContext.Comentario.Add(comentario);
            await dbContext.SaveChangesAsync();
        }

    }
}
