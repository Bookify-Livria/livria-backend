namespace LivriaBackend.communities.Domain.Model.Commands
{
    public record CreatePostCommand(
        int CommunityId,
        string Username, // El username del UserClient que crea el post
        string Content,
        string Img
    );
}