using LivriaBackend.commerce.Domain.Model.Aggregates; 

namespace LivriaBackend.commerce.Domain.Model.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; } 

        public int BookId { get; private set; } 
       

        public string BookTitle { get; private set; }
        public string BookAuthor { get; private set; }
        public decimal BookPrice { get; private set; } 
        public string BookCover { get; private set; } 

        public int Quantity { get; private set; } 
        public decimal ItemTotal { get; private set; } 

        public int OrderId { get; private set; } 
        public Order Order { get; private set; } 

        
        protected OrderItem() { }

        
        public OrderItem(int bookId, string bookTitle, string bookAuthor, decimal bookPrice, string bookCover, int quantity)
        {
            
            if (string.IsNullOrWhiteSpace(bookTitle)) throw new ArgumentNullException(nameof(bookTitle));
            if (string.IsNullOrWhiteSpace(bookAuthor)) throw new ArgumentNullException(nameof(bookAuthor));
            if (bookPrice <= 0) throw new ArgumentOutOfRangeException(nameof(bookPrice), "Book price must be positive.");
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

            BookId = bookId;
            BookTitle = bookTitle;
            BookAuthor = bookAuthor;
            BookPrice = bookPrice;
            BookCover = bookCover;
            Quantity = quantity;
            ItemTotal = quantity * bookPrice;
        }

        
        internal void SetOrder(Order order)
        {
            Order = order;
            OrderId = order.Id; 
        }
    }
}