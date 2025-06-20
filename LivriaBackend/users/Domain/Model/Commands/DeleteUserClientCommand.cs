namespace LivriaBackend.users.Domain.Model.Commands
{
    public class DeleteUserClientCommand
    {
        public int UserClientId { get; }

        public DeleteUserClientCommand(int userClientId)
        {
            UserClientId = userClientId;
        }
    }
}