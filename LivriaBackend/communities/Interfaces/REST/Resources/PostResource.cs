using System;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record PostResource(
        int Id,
        int CommunityId,
        int UserId, 
        string Username,
        string Content,
        string Img,
        DateTime CreatedAt 
    );
}