using LivriaBackend.commerce.Domain.Model.Entities;
using LivriaBackend.commerce.Domain.Model.ValueObjects;
using LivriaBackend.users.Domain.Model.Aggregates; 

namespace LivriaBackend.commerce.Domain.Model.Aggregates
{
    public class Order
    {
        public int Id { get; private set; }
        public string Code { get; private set; } 
        public int UserClientId { get; private set; } 
        public UserClient UserClient { get; private set; } 
        
        public string UserEmail { get; private set; }
        public string UserPhone { get; private set; }
        public string UserFullName { get; private set; }

        public bool IsDelivery { get; private set; } 
        
        public Shipping Shipping { get; private set; } 

        public decimal Total { get; private set; }
        public DateTime Date { get; private set; } 

        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        protected Order() { }

        public Order(
            int userClientId,
            string userEmail,
            string userPhone,
            string userFullName,
            bool isDelivery,
            Shipping shipping, 
            List<OrderItem> orderItems) 
        {
            if (userClientId <= 0) throw new ArgumentOutOfRangeException(nameof(userClientId), "UserClient ID must be positive.");
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentNullException(nameof(userEmail), "User email cannot be empty.");
            if (string.IsNullOrWhiteSpace(userPhone)) throw new ArgumentNullException(nameof(userPhone), "User phone cannot be empty.");
            if (string.IsNullOrWhiteSpace(userFullName)) throw new ArgumentNullException(nameof(userFullName), "User full name cannot be empty.");
            if (orderItems == null || !orderItems.Any()) throw new ArgumentException("Order must contain at least one item.", nameof(orderItems));
            if (isDelivery && shipping == null) throw new ArgumentException("Shipping details are required for delivery orders.");
            if (!isDelivery && shipping != null) throw new ArgumentException("Shipping details should be null for non-delivery orders.");


            UserClientId = userClientId;
            UserEmail = userEmail;
            UserPhone = userPhone;
            UserFullName = userFullName;
            IsDelivery = isDelivery;
            Shipping = shipping; 

            Code = GenerateOrderCode();
            Date = DateTime.UtcNow; 

            foreach (var item in orderItems)
            {
                _items.Add(item);
                Total += item.ItemTotal;
                item.SetOrder(this); 
            }
        }

        public static string GenerateOrderCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }

    }
}