using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CartItemResource(
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int Id,
        
        BookResource Book, 
        
<<<<<<< HEAD
        int Quantity,
        
=======
        [Range(0, 3, ErrorMessage = "RangeError")]
        int Quantity,
        
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        int UserClientId 
    );
}