namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record ReviewResource(
        int Id,
        int BookId,
        string Username, 
        string Content
    );
}