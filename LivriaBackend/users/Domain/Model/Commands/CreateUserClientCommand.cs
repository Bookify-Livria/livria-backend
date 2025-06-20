namespace LivriaBackend.users.Domain.Model.Commands
{
    
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