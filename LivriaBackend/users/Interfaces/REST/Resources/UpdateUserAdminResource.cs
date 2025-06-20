using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UpdateUserAdminResource(
        [Required] string Display,
        [Required] string Username,
        [Required][EmailAddress] string Email,
        [Required] string Password,
        [Required] bool AdminAccess,
        string SecurityPin
    );
}