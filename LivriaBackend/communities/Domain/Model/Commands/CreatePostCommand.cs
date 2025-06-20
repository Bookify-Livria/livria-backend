namespace LivriaBackend.communities.Domain.Model.Commands
{
    public record CreatePostCommand(
        int CommunityId,
        string Username, 
        string Content,
        string Img
    );
}