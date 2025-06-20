using System.Collections.Generic;
using LivriaBackend.commerce.Domain.Model.Entities;

namespace LivriaBackend.commerce.Domain.Model.Aggregates
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Cover { get; private set; }
        public string Genre { get; private set; }
        public string Language { get; private set; }

        public ICollection<Review> Reviews { get; private set; } = new List<Review>();

        protected Book() { }

        public Book(string title, string description, string author, decimal price, int stock, string cover, string genre, string language)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Stock = stock; 
            Cover = cover;
            Genre = genre;
            Language = language;
        }
        
        public void DecreaseStock(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity to decrease cannot be negative.");
            }
            if (Stock < quantity)
            {
              
                throw new InvalidOperationException($"Insufficient stock. Available: {Stock}, Requested: {quantity}.");
            }
            Stock -= quantity;
        }

        public void Update(string title, string description, string author, decimal price, int stock, string cover, string genre, string language)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Stock = stock;
            Cover = cover;
            Genre = genre;
            Language = language;
        }

        public void AddReview(Review review)
        {
            if (review != null && review.BookId == this.Id)
            {
                Reviews.Add(review);
            }
        }
    }
}