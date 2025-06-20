namespace LivriaBackend.users.Domain.Model.Commands
{
    // Cambiado a 'public record'
    public record DeleteUserClientCommand(
        int UserClientId
    );
}