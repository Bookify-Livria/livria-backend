using LivriaBackend.commerce.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Aggregates; 

namespace LivriaBackend.commerce.Domain.Model.Entities
{
    public class Review
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Content { get; private set; }

        public int Stars { get; private set; }

        public int BookId { get; private set; }
        public Book Book { get; private set; } 
        
        public int UserClientId { get; private set; } 
        public UserClient UserClient { get; private set; } 

        protected Review() { }
        
        public Review(int bookId, int userClientId, string content, int stars, string username)
        {
            BookId = bookId;
            UserClientId = userClientId;
            Content = content;
            Stars = stars;
            Username = username; 
        }
    }
}