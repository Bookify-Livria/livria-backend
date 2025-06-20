namespace LivriaBackend.users.Domain.Model.Commands
{
    public class UpdateUserCommand
    {
        public int UserId { get; }
        public string Display { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }

        public UpdateUserCommand(int userId, string display, string username, string email, string password)
        {
            UserId = userId;
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}