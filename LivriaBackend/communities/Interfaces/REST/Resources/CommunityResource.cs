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
        
<<<<<<< HEAD
        /* [Url(ErrorMessage = "UrlError")] */
        string Image,
        
=======
        [Url(ErrorMessage = "UrlError")]
        string Image,
        
        [Url(ErrorMessage = "UrlError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Banner,
        
        IEnumerable<PostResource> Posts 
    );
}