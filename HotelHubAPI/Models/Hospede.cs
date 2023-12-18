namespace HotelHubAPI.Models
{
    public class Hospede
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}
