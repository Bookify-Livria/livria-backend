using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record CreateUserClientResource(
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,

        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Email,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "LengthError")]
        string Password,

        [Url(ErrorMessage = "UrlError")]
        string Icon, 
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Phrase 
        
        
    );
}