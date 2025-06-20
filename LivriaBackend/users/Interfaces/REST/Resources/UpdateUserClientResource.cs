using System.ComponentModel.DataAnnotations; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record class UpdateUserClientResource : UpdateUserResource
    {
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Icon { get; init; }
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Phrase { get; init; }
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Subscription { get; init; }

        
        public UpdateUserClientResource(
            string? display,
            string? username,
            string? email,
            string? password,
            string? icon,
            string? phrase,
            string? subscription
        ) : base(display, username, email, password, icon, phrase)
        {
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
        }
        
        
        public UpdateUserClientResource() : base() { }
    }
}