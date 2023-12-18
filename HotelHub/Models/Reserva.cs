using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class Reserva {
        public int ReservaId { get; set; }

        [Required]
        public DateTime DataEntrada { get; set; }

        [Required]
        public DateTime DataSaida { get; set; }

        public string Observacao { get; set; } = string.Empty;

        public Hospede Hospede { get; set; }
        public Quarto Quarto { get; set; }
        public Hotel Hotel { get; set; }

    }
}
