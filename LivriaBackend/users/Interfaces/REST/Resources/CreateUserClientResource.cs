using System.ComponentModel.DataAnnotations; // Para validaciones

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class CreateUserClientResource
    {
        [Required]
        [StringLength(100)]
        public string Display { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)] // Ejemplo de validación para contraseña
        public string Password { get; set; }

        public string Icon { get; set; }
        public string Phrase { get; set; }
        public string Subscription { get; set; }
    }
}