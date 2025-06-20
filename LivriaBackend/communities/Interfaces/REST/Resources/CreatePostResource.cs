using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record CreatePostResource(
        [Required(ErrorMessage = "EmptyField")]
        string Username, 
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(500, ErrorMessage = "MaxLengthError")]
        string Content,
        
        /* [Url(ErrorMessage = "UrlError")] */
        string Img
    );
}