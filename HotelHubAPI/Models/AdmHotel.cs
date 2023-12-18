namespace HotelHubAPI.Models
{
    public class AdmHotel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Hotel> Hoteis { get; set; }
    }
}
