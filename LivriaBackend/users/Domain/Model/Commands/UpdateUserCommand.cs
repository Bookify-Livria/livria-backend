namespace LivriaBackend.users.Domain.Model.Commands
{
    // Cambiado a 'public record'
    public record UpdateUserCommand(
        int UserId,
        string Display,
        string Username,
        string Email,
        string Password
    );
}