using System.Collections.Generic;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for representing a Community (response DTO).
    /// </summary>
    public record CommunityResource(
        int Id,
        string Name,
        string Description,
        string Type,
        string Image,
        string Banner,
        // You can include a list of PostResource here if you want posts to be returned with the community
        IEnumerable<PostResource> Posts // Add this line if you want to include posts
    );
}