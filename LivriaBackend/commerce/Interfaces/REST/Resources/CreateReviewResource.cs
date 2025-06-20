using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateReviewResource(
        [Required(ErrorMessage = "EmptyField")]
        int BookId,
        [Required(ErrorMessage = "EmptyField")]
        int UserClientId, 
        [Required(ErrorMessage = "EmptyField")]
        string Content,
        [Required(ErrorMessage = "EmptyField")]
        [Range(1, 5)]
        int Stars
    );
}