using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace LivriaBackend.users.Domain.Model.Aggregates;

public class UserClient : User
{
    public string Icon { get; set; } // Asegúrate de que tenga un set;
    public string Phrase { get; set; } // Asegúrate de que tenga un set;
    public List<int> Order { get; private set; } // Esto es 'private set'. Lee la nota en AppDbContext sobre mapeo de List<int>.
    public string Subscription { get; set; }

    [JsonConstructor]
    public UserClient(int id, string display, string username, string email, string password,
        string icon, string phrase, List<int> order, string subscription)
        : base(id, display, username, email, password)
    {
        Icon = icon;
        Phrase = phrase;
        Order = order ?? new List<int>();
        Subscription = subscription;
    }

    public UserClient(string display, string username, string email, string password,
        string icon, string phrase, string subscription)
        : base(display, username, email, password)
    {
        Icon = icon;
        Phrase = phrase;
        Order = new List<int>();
        Subscription = subscription;
    }

    protected UserClient() : base() { Order = new List<int>(); }

    public void Update(string display, string username, string email, string password, string icon, string phrase, string subscription)
    {
        base.Update(display, username, email, password);
        Icon = icon;
        Phrase = phrase;
        Subscription = subscription;
    }

    public void AddOrder(int orderId)
    {
        if (!Order.Contains(orderId))
        {
            Order.Add(orderId);
        }
    }

    public void RemoveOrder(int orderId)
    {
        Order.Remove(orderId);
    }
}