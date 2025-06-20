using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record ReviewResource(
        
        int Id,
        
        int BookId,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Username, 
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Content,
        
        int Stars
        
        
    );
}