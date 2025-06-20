using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateBookResource(
        [Required] [StringLength(255)] string Title,
        [StringLength(1000)] string Description,
        [Required] [StringLength(100)] string Author,
        [Required] [Range(0.01, 10000.00)] decimal Price, 
        [Required] [Range(0, 10000)] int Stock,          
        [StringLength(255)] string Cover,
        [StringLength(50)] string Genre,
        [StringLength(50)] string Language
    );
}