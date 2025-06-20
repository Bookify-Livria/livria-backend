namespace LivriaBackend.users.Domain.Model.Commands
{
    public record UpdateUserClientCommand(
        int UserClientId,
        string Display,
        string Username,
        string Email,
        string Password,
        string Icon,
        string Phrase,
        string Subscription
    );
}