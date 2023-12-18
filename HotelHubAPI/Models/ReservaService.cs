using HotelHubAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelHubAPI.Models {
    public class ReservaService {
        private readonly HotelHubAPIContext _context;

        // Injeção do contexto do banco de dados

        public async Task<bool> CriarReserva(int quartoId, Reserva novaReserva) {
            var quarto = await _context.Quarto
                .Include(q => q.Reserva)
                .FirstOrDefaultAsync(q => q.Id == quartoId);

            if (quarto == null) {
                // Lógica para lidar com o quarto não encontrado
                return false;
            }

            if (quarto.Reserva != null) {
                // Quarto já está reservado para o período escolhido
                return false;
            }

            novaReserva.QuartoId = quartoId;
            _context.Reserva.Add(novaReserva);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
