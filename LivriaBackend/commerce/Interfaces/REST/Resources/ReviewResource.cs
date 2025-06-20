using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record ReviewResource(
<<<<<<< HEAD
        
        int Id,
        
=======
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int Id,
        
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        int BookId,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Username, 
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
<<<<<<< HEAD
        string Content
=======
        string Content,
        
        [Range(0, 5, ErrorMessage = "RangeError")]
        int Stars
        
        
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
    );
}