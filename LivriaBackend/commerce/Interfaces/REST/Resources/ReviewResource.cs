using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record ReviewResource(
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int Id,
        
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int BookId,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Username, 
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Content,
        
        [Range(0, 5, ErrorMessage = "RangeError")]
        int Stars
        
        
    );
}