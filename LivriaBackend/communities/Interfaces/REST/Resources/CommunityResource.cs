using System.Collections.Generic;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{

    public record CommunityResource(
        int Id,
        string Name,
        string Description,
        string Type,
        string Image,
        string Banner,
        
        IEnumerable<PostResource> Posts 
    );
}