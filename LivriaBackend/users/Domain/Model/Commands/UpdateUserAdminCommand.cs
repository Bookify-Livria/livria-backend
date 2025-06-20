namespace LivriaBackend.users.Domain.Model.Commands
{
    public record UpdateUserAdminCommand(
        int UserAdminId,
        string Display,
        string Username,
        string Email,
        string Password,
        bool AdminAccess,
        string SecurityPin
    );
}