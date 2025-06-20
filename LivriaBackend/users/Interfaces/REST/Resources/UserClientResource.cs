using System.Collections.Generic;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record'
    public record UserClientResource(
        int Id,
        string Display,
        string Username,
        string Email,
        string Icon,
        string Phrase,
        List<int> Order, // O un DTO más simple para los elementos de Order
        string Subscription
    ) : UserResource(Id, Display, Username, Email); // Hereda del constructor primario de UserResource
}