using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    public record CreatePostResource(
        [Required(ErrorMessage = "EmptyField")]
        string Username, 
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(500, ErrorMessage = "MaxLengthError")]
        string Content,
        
<<<<<<< HEAD
        /* [Url(ErrorMessage = "UrlError")] */
=======
        [Url(ErrorMessage = "UrlError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Img
    );
}