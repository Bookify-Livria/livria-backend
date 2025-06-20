using LivriaBackend.commerce.Domain.Model.Aggregates; // Para Book
using LivriaBackend.users.Domain.Model.Aggregates; // Para UserClient

namespace LivriaBackend.commerce.Domain.Model.Entities
{
    public class CartItem
    {
        public int Id { get; private set; } 

        public int BookId { get; private set; }
        public Book Book { get; private set; }

        public int Quantity { get; private set; } 

        public int UserClientId { get; private set; } 
        public UserClient UserClient { get; private set; } 

        protected CartItem() { }

        public CartItem(int bookId, int quantity, int userClientId)
        {
            BookId = bookId;
            Quantity = quantity;
            UserClientId = userClientId;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity cannot be negative.");
            }
            Quantity = newQuantity;
        }

        public void IncrementQuantity(int amount = 1)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to increment cannot be negative.");
            }
            Quantity += amount;
        }

        public void DecrementQuantity(int amount = 1)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to decrement cannot be negative.");
            }
            Quantity -= amount;
            if (Quantity < 0) Quantity = 0;
        }
    }
}