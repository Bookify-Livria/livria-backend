using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record class UpdateUserResource
    {
      
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string? Display { get; init; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        public string? Username { get; init; }
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        public string? Email { get; init; }
        [StringLength(255, MinimumLength = 6, ErrorMessage = "LengthError")]
        public string? Password { get; init; } 
        public string? Icon { get; init; } 
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Phrase { get; init; } 

        
        public UpdateUserResource(string? display, string? username, string? email, string? password, string? icon, string? phrase)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            Icon = icon;
            Phrase = phrase;
        }

        
        public UpdateUserResource() { }
    }
}