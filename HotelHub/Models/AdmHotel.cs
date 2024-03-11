using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public class AdmHotel : Usuario {
        public ICollection<Hotel> HoteisGerenciados { get; set; } = new List<Hotel>();
    }
}
