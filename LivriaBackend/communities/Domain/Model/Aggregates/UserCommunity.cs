using LivriaBackend.users.Domain.Model.Aggregates; // For UserClient
using LivriaBackend.communities.Domain.Model.Aggregates; // For Community

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the many-to-many relationship between UserClient and Community.
    /// </summary>
    public class UserCommunity
    {
        public int UserClientId { get; private set; } // Foreign key to UserClient
        public UserClient UserClient { get; private set; } // Navigation property to UserClient

        public int CommunityId { get; private set; } // Foreign key to Community
        public Community Community { get; private set; } // Navigation property to Community

        // Optional: Additional properties for the relationship, e.g., JoinDate
        public DateTime JoinedDate { get; private set; }

        // Constructor for EF Core
        private UserCommunity() { }

        public UserCommunity(int userClientId, int communityId)
        {
            UserClientId = userClientId;
            CommunityId = communityId;
            JoinedDate = DateTime.UtcNow; // Set creation date
        }
    }
}