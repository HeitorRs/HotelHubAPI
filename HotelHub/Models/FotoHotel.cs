using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class FotoHotel {
        public int FotoId { get; set; }
        [Required]
        public string NomeArquivo { get; set; }

        public Hotel Hotel { get; set; }
    }
}
