using System.Collections.Generic; // Si se necesita para alguna propiedad compleja en el futuro

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record'
    public record UserAdminResource(
        int Id,
        string Display,
        string Username,
        string Email,
        bool AdminAccess,
        string SecurityPin // Considera si realmente quieres exponer el SecurityPin
    ) : UserResource(Id, Display, Username, Email); // Hereda del constructor primario de UserResource
}