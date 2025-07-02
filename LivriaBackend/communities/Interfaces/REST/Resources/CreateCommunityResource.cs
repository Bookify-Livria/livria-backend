using System.ComponentModel.DataAnnotations;
using LivriaBackend.communities.Domain.Model.ValueObjects; // ¡Nuevo using!

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
        CommunityType Type, 
        
        [Url(ErrorMessage = "UrlError")]
        string? Image, 
        
        [Url(ErrorMessage = "UrlError")]
        string? Banner 
    );
}