using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record RecommendationResource(
<<<<<<< HEAD
        int UserClientId,
=======
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int UserClientId,
        
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        IEnumerable<BookResource> RecommendedBooks
    );
}