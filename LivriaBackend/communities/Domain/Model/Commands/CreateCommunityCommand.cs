namespace LivriaBackend.communities.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new Community.
    /// </summary>
    public record CreateCommunityCommand(
        string Name,
        string Description,
        string Type,
        string Image,
        string Banner
    );
}