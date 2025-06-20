using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateCartItemResource(
        [Required(ErrorMessage = "EmptyField")]
        int BookId,
        
        [Required(ErrorMessage = "EmptyField")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeError")]
        int Quantity,
        
        [Required(ErrorMessage = "EmptyField")]
<<<<<<< HEAD
=======
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        int UserClientId
    );
}