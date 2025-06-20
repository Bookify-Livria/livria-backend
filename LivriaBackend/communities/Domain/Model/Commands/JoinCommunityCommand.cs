namespace LivriaBackend.communities.Domain.Model.Commands
{

    public record JoinCommunityCommand(
        int UserClientId,
        int CommunityId
    );
}