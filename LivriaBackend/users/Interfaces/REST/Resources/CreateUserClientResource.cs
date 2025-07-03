using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record CreateUserClientResource
    {
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string Display { get; init; }
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string Username { get; init; }

        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        public string Email { get; init; }

        [Url(ErrorMessage = "UrlError")]
        public string Icon { get; init; }
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string Phrase { get; init; }
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "LengthError")]
        public string Password { get; init; }
        
        [Required(ErrorMessage = "EmptyField")]
        [Compare(nameof(Password), ErrorMessage = "PasswordMismatch")]
        public string ConfirmPassword { get; init; }
    }
}