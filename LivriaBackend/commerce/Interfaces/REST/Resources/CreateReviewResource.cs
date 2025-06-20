using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateReviewResource(
        [Required] int BookId,
        [Required] int UserClientId, 
        [Required] string Content
    );
}