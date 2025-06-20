namespace LivriaBackend.users.Domain.Model.Commands
{
    public class CreateUserClientCommand
    {
        public string Display { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string Icon { get; }
        public string Phrase { get; }
        public string Subscription { get; }

        public CreateUserClientCommand(string display, string username, string email, string password,
            string icon, string phrase, string subscription)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
        }
    }
}