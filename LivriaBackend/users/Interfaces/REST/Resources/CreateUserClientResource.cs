using System.ComponentModel.DataAnnotations; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record CreateUserClientResource(
        [Required] [StringLength(100)] string Display,
        [Required] [StringLength(50)] string Username,
        [Required] [EmailAddress] [StringLength(100)] string Email,
        [Required] [MinLength(6)] string Password, 
        string Icon,
        string Phrase,
        string Subscription
    );
}