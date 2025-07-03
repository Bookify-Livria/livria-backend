using System.ComponentModel.DataAnnotations;
using LivriaBackend.Shared.Resources;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record RegisterUserClientRequest
    {
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string Username { get; init; }

        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "LengthError")]
        [DataType(DataType.Password)]
        public string Password { get; init; }
        
        [Required(ErrorMessage = "EmptyField")]
        [Compare(nameof(Password), ErrorMessage = "PasswordMismatch")]
        public string ConfirmPassword { get; init; }
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string Display { get; init; }

        [Required(ErrorMessage = "EmptyField")]
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        public string Email { get; init; }

        [Url(ErrorMessage = "UrlError")]
        public string Icon { get; init; }
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string Phrase { get; init; }
    }
}