using System.Collections.Generic;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record OrderResource(
        int Id,
        string Code,
        int UserClientId,
        string UserEmail,
        string UserPhone,
        string UserFullName,
        bool IsDelivery,
        ShippingResource Shipping, 
        decimal Total,
        string Date, 
        IEnumerable<OrderItemResource> Items
    );
}