namespace HotelHubAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int NumPessoas { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int QuartoId { get; set; }
        public Quarto Quarto { get; set; }

        public int HospedeId { get; set; }
        public Hospede Hospede { get; set; }
    }
}
