using System.Collections.Generic;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record RecommendationResource(
        int UserClientId,
        IEnumerable<BookResource> RecommendedBooks
    );
}