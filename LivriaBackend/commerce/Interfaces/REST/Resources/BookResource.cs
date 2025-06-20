using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivriaBackend.commerce.Interfaces.REST.Resources; 

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record BookResource(
        int Id,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Title,
        
        [StringLength(500, ErrorMessage = "MaxLengthError")]
        string Description,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Author,
        
        [Range(0.01, 10000.00, ErrorMessage = "RangeError")]
        decimal Price,
        
        [Range(0, 10000, ErrorMessage = "RangeError")]
        int Stock,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Cover,
        
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Genre,
        
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Language,
        
        IEnumerable<ReviewResource> Reviews 
    );
}