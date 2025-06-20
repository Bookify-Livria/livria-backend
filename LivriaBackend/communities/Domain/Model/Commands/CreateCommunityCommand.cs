namespace LivriaBackend.communities.Domain.Model.Commands
{

    public record CreateCommunityCommand(
        string Name,
        string Description,
        string Type,
        string Image,
        string Banner
    );
}