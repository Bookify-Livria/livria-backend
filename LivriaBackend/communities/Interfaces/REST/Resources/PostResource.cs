using System;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record PostResource(
        int Id,
        int CommunityId,
        int UserId, // Ahora se expone el UserId
        string Username,
        string Content,
        string Img,
        DateTime CreatedAt // Si añadiste CreatedAt en Post.cs
    );
}