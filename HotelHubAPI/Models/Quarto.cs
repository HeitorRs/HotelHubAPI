namespace HotelHubAPI.Models
{
    public class Quarto
    {
        public int Id { get; set; }
        public bool Disponibilidade { get; set; }
        public float Valor { get; set; }
        public int Numero { get; set; }
        public int Camas { get; set; }
        public int Banheiros { get; set; }
        public string Descricao { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int? ReservaId { get; set; }
        public Reserva Reserva { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
    }
}
