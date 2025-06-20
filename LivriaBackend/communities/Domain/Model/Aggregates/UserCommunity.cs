using LivriaBackend.users.Domain.Model.Aggregates; 
using LivriaBackend.communities.Domain.Model.Aggregates; 

namespace LivriaBackend.communities.Domain.Model.Aggregates
{

    public class UserCommunity
    {
        public int UserClientId { get; private set; } 
        public UserClient UserClient { get; private set; } 

        public int CommunityId { get; private set; } 
        public Community Community { get; private set; } 

        public DateTime JoinedDate { get; private set; }

        private UserCommunity() { }

        public UserCommunity(int userClientId, int communityId)
        {
            UserClientId = userClientId;
            CommunityId = communityId;
            JoinedDate = DateTime.UtcNow; 
        }
    }
}