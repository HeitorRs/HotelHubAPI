using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class Reserva {
        public int ReservaId { get; set; }

        [Required]
        public DateTime DataEntrada { get; set; }

        [Required]
        public DateTime DataSaida { get; set; }

        public string Observacao { get; set; } = string.Empty;

        public float ValorTotal { get; set; }

        public Hospede Hospede { get; set; }
        public Quarto Quarto { get; set; }
        public Hotel Hotel { get; set; }

        public void CalcularValorTotal() {
            if (DataEntrada < DataSaida) {

                TimeSpan duracao = DataSaida - DataEntrada;
                int numeroDias = duracao.Days;

                ValorTotal = numeroDias * Quarto.Preco;
            } else {
                throw new ArgumentException("Data de entrada deve ser anterior à data de saída.");
            }
        }
    }
}
