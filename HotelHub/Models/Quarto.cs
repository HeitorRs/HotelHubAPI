using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static HotelHub.Models.Comentario;

namespace HotelHub.Models {
    public class Quarto {
        public int QuartoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public float Preco { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Hotel Hotel { get; set; }

        public ICollection<Foto> FotosQuarto { get; set; }

        public ICollection<Comentario> ComentariosQuarto { get; set; } = new List<Comentario>();
    }
}
