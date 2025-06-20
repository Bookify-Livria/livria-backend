namespace LivriaBackend.users.Interfaces.REST.Resources
{
    // Cambiado a 'public record'
    public record UserResource(
        int Id,
        string Display,
        string Username,
        string Email
    );
    // La contraseña NUNCA debe exponerse en un recurso de respuesta
}