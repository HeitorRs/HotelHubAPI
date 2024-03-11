using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class Hospede : Usuario {
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();  
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    }
}
