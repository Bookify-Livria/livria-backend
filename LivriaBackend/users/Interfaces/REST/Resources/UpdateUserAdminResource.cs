using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UpdateUserAdminResource(
        [Required(ErrorMessage = "EmptyField")]
<<<<<<< HEAD
        string Display,
        
        [Required(ErrorMessage = "EmptyField")]
        string Username,
        
        [Required(ErrorMessage = "EmptyField")]
=======
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        string Email,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "LengthError")]
        string Password,
        
        [Required(ErrorMessage = "EmptyField")] bool AdminAccess,
        string SecurityPin
    );
}