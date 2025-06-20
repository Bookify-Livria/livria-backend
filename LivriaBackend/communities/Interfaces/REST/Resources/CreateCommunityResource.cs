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
        
        [Url(ErrorMessage = "UrlError")]
        string Image,
        
        [Url(ErrorMessage = "UrlError")]
        string Banner
    );
}