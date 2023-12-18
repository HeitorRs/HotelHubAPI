using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class FotoQuarto {
        public int FotoId { get; set; }
        [Required]
        public string NomeArquivo { get; set; }

        public Quarto Quarto { get; set; }
    }
}
