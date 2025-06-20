namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record UserResource(
        int Id,
        string Display,
        string Username,
        string Email
    );
    
}