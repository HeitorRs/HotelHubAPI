using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class Comentario {
        public int ComentarioId { get; set; }

        public string? Texto { get; set; }

        public Hospede? Hospede { get; set; }
        public Quarto? Quarto { get; set; }

    }
}
