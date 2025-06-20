namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record CreatePostResource(
        string Username, // El username del UserClient que crea el post
        string Content,
        string Img
    );
}