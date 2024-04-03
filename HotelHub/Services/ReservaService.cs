using HotelHub.Data;
using HotelHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static HotelHub.Controllers.ReservasController;

namespace HotelHub.Services {
    public class ReservaService {
        private readonly HotelHubContext _context;

        public ReservaService(HotelHubContext context) {
            _context = context;
        }
        public ReservaService() { }

        public async Task<List<Reserva>> GetReservas() {
            var reservas = await _context.Reserva.ToListAsync();

            if (_context.Reserva == null) {
                return null;
            }
            return reservas;
        }

        public async Task<Reserva> GetReserva(int id) {
            try {
                Reserva reserva = _context.Reserva.Include(r => r.Hotel).ThenInclude(r => r.Quartos).FirstOrDefault(r => r.ReservaId == id);
                return reserva;

            } catch (Exception ex) {
                return null;
            }
        }

        public async Task<List<Reserva>> GetReservasHospede(int hospedeId) {
            try {
                List<Reserva> reservas = await _context.Reserva.Include(r => r.Hotel).ThenInclude(r => r.Quartos).Where(r => r.Hospede.UserId == hospedeId).ToListAsync();
                return reservas;

            } catch (Exception ex) {
                return null;
            }
        }

        public async Task<List<ReservaDataModel>> GetReservasPorQuarto(int quartoId) {
            var datasreservadas = new List<ReservaDataModel>();
            try {
                var reservas = await _context.Reserva.Where(r => r.Quarto.QuartoId == quartoId).ToListAsync();
                foreach (var reserva in reservas) {
                    var datareservada = new ReservaDataModel {
                        DataEntrada = reserva.DataEntrada.ToString("yyyy-MM-dd"),
                        DataSaida = reserva.DataSaida.ToString("yyyy-MM-dd")
                    };
                    datasreservadas.Add(datareservada);
                }
                return datasreservadas;
            } catch (Exception ex) {
                return null;
            }
        }

        public async Task<bool> PostReserva(ReservaModel model) {
            try {
                DateTime dataEntrada = DateTime.ParseExact(model.dataentrada, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime dataSaida = DateTime.ParseExact(model.datasaida, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                Hospede hospede = await _context.Hospede.FindAsync(model.hospedeid);
                Quarto quarto = await _context.Quarto.FindAsync(model.quartoid);
                Hotel hotel = await _context.Hotel.FindAsync(model.hotelid);

                if (hotel == null || quarto == null || hospede == null) {
                    return false;
                }

                var reserva = new Reserva {
                    DataEntrada = dataEntrada,
                    DataSaida = dataSaida,
                    Observacao = model.observacao,
                    Hotel = hotel,
                    Quarto = quarto,
                    Hospede = hospede
                };

                reserva.CalcularValorTotal();

                _context.Reserva.Add(reserva);
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public async Task<bool> DeleteReservas(int id) {
            if (_context.Reserva == null) {
                return false;
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null) {
                return false;
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();

            return true;
        }

        public class ReservaDataModel {
            public string DataEntrada { get; set; }
            public string DataSaida { get; set; }
        }

        public class ReservaModel {
            public string dataentrada { get; set; }
            public string datasaida { get; set; }
            public string observacao { get; set; }
            public int hotelid { get; set; }
            public int quartoid { get; set; }
            public int hospedeid { get; set; }
        }
    }
}
