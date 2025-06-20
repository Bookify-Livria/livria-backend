using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateReviewResource(
        [Required(ErrorMessage = "EmptyField")]
<<<<<<< HEAD
        int BookId,
        [Required(ErrorMessage = "EmptyField")]
        int UserClientId, 
        [Required(ErrorMessage = "EmptyField")]
        string Content
=======
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int BookId,
        
        [Required(ErrorMessage = "EmptyField")]
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int UserClientId, 
        
        [Required(ErrorMessage = "EmptyField")]
        string Content,
        
        [Required(ErrorMessage = "EmptyField")]
        [Range(1, 5)]
        int Stars
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
    );
}