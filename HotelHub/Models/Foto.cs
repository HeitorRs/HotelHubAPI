using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class Foto {
        public int FotoId { get; set; }

        [Required]
        public string NomeArquivo { get; set; }

    }
}
