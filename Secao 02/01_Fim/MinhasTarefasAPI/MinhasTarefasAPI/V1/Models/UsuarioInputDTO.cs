using System.ComponentModel.DataAnnotations;

namespace MinhasTarefasAPI.V1.Models
{
    public class UsuarioInputDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
