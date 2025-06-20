using System.ComponentModel.DataAnnotations; // Para validaciones

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class UpdateUserClientResource
    {
        [Required]
        [StringLength(100)]
        public string Display { get; set; }

        [StringLength(50)]
        public string Username { get; set; } // Puede que no quieras que el username se pueda actualizar

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } // Puede que no quieras que el email se pueda actualizar

        public string Password { get; set; } // Considerar si la contraseña se actualiza aquí o en un endpoint aparte
        public string Icon { get; set; }
        public string Phrase { get; set; }
        public string Subscription { get; set; }
    }
}