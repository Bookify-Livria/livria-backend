using System.Collections.Generic;
using LivriaBackend.commerce.Domain.Model.Aggregates;

namespace LivriaBackend.commerce.Domain.Model.Entities
{
    
    
    public class Recommendation
    {
        public int UserClientId { get; private set; }
        public IEnumerable<Book> RecommendedBooks { get; private set; }

        public Recommendation(int userClientId, IEnumerable<Book> recommendedBooks)
        {
            UserClientId = userClientId;
            RecommendedBooks = recommendedBooks ?? new List<Book>(); 
        }
    }
}