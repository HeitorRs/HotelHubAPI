using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public abstract class Foto {
        [Key]
        public int FotoId { get; set; }

        [Required]
        public string NomeArquivo { get; set; }

    }
}
