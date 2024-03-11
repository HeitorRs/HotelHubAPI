using System.ComponentModel.DataAnnotations;
using static HotelHub.Models.Comentario;

namespace HotelHub.Models {
    public class Hotel {
        public int HotelId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string Cidade { get; set; }

        public ICollection<Quarto> Quartos { get; set; } = new List<Quarto>();

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public AdmHotel Administrador { get; set; }

        public ICollection<Foto> FotosHotel { get; set; }
    }
}
