using System.ComponentModel.DataAnnotations;

namespace HotelHub.Models {
    public abstract class Usuario {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }

        public TipoUsuario Tipo { get; set; }

        public Usuario() { }
    }
}
