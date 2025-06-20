using System.Collections.Generic;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public class UserClientResource
    {
        public int Id { get; set; }
        public string Display { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }
        public string Phrase { get; set; }
        public List<int> Order { get; set; } // O un DTO más simple para los elementos de Order
        public string Subscription { get; set; }
    }
}