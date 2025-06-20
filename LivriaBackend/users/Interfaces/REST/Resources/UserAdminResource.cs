using System.Collections.Generic; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record'
    public record UserAdminResource(
        int Id,
        string Display,
        string Username,
        string Email,
        bool AdminAccess,
        string SecurityPin 
    ) : UserResource(Id, Display, Username, Email); 
}