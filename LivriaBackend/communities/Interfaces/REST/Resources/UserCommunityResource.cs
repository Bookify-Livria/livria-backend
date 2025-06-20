using System;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for representing a User-Community membership.
    /// </summary>
    public record UserCommunityResource(
        int UserClientId,
        int CommunityId,
        DateTime JoinedDate
    );
}