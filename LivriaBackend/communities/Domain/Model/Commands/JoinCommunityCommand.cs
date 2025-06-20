namespace LivriaBackend.communities.Domain.Model.Commands
{
    /// <summary>
    /// Command to allow a UserClient to join a Community.
    /// </summary>
    public record JoinCommunityCommand(
        int UserClientId,
        int CommunityId
    );
}