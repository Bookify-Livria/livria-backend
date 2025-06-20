using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for a UserClient to join a Community.
    /// </summary>
    public record JoinCommunityResource(
        [Required] int UserClientId,
        [Required] int CommunityId
    );
}