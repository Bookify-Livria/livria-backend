namespace LivriaBackend.users.Domain.Model.Queries
{
    public class GetUserClientByIdQuery
    {
        public int UserClientId { get; }

        public GetUserClientByIdQuery(int userClientId)
        {
            UserClientId = userClientId;
        }
    }
}