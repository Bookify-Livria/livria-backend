using System;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{

    public record UserCommunityResource(
        int UserClientId,
        int CommunityId,
        DateTime JoinedDate
    );
}