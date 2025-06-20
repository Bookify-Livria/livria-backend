using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UpdateUserAdminResource(
        [Required(ErrorMessage = "EmptyField")]
        string Display,
        
        [Required(ErrorMessage = "EmptyField")]
        string Username,
        
        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        string Email,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "LengthError")]
        string Password,
        
        [Required(ErrorMessage = "EmptyField")] bool AdminAccess,
        string SecurityPin
    );
}