using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateBookResource(
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Title,
        
        [StringLength(1000, ErrorMessage = "MaxLengthError")]
        string Description,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Author,
        
        [Required(ErrorMessage = "EmptyField")]
        [Range(0.01, 10000.00, ErrorMessage = "RangeError")]
        decimal Price,
        
        [Required(ErrorMessage = "EmptyField")]
        [Range(0, 10000, ErrorMessage = "RangeError")]
        int Stock,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        [Url(ErrorMessage = "UrlError")]
        string Cover,
        
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Genre,
        
        [StringLength(50, ErrorMessage = "MaxLengthError")]
        string Language
    );
}