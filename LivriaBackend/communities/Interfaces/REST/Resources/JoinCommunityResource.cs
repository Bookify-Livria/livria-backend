using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{

    public record JoinCommunityResource(
        [Required] int UserClientId,
        [Required] int CommunityId
    );
}