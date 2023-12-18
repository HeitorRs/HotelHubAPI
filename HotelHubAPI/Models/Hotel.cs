namespace HotelHubAPI.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }

        public int AdmHotelId { get; set; }
        public AdmHotel AdmHotel { get; set; }

        public ICollection<Quarto> Quartos { get; set; }
    }
}
