namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record CreatePostResource(
        string Username, 
        string Content,
        string Img
    );
}