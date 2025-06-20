using System.ComponentModel.DataAnnotations;
using Mysqlx;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{

    public record CreateCommunityResource(
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Name,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(500, ErrorMessage = "MaxLengthError")]
        string Description,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Type,
        
<<<<<<< HEAD
        /* [Url(ErrorMessage = "UrlError")] */
        string Image,
        
        /* [Url(ErrorMessage = "UrlError")] */
=======
        [Url(ErrorMessage = "UrlError")]
        string Image,
        
        [Url(ErrorMessage = "UrlError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Banner
    );
}