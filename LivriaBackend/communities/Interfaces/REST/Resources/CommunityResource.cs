using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{

    public record CommunityResource(
        int Id,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Name,
        
        [StringLength(500, ErrorMessage = "MaxLengthError")]
        string Description,
        
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Type,
        
        /* [Url(ErrorMessage = "UrlError")] */
        string Image,
        
        string Banner,
        
        IEnumerable<PostResource> Posts 
    );
}