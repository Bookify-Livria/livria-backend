namespace LivriaBackend.users.Domain.Model.Queries
{
    // Cambiado a 'public record'
    public record GetUserClientByIdQuery(
        int UserClientId
    );
}