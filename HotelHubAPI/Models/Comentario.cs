namespace HotelHubAPI.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public float Nota { get; set; }
        public string Descricao { get; set; }

        public int? HospedeId { get; set; }
        public Hospede Hospede { get; set; }

        public int? QuartoId { get; set; }
        public Quarto Quarto { get; set; }
    }
}
