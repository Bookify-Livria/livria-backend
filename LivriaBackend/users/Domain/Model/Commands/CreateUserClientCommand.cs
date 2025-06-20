namespace LivriaBackend.users.Domain.Model.Commands
{
    // Cambiado a 'public record'
    public record CreateUserClientCommand(
        string Display,
        string Username,
        string Email,
        string Password,
        string Icon,
        string Phrase,
        string Subscription
    );
}