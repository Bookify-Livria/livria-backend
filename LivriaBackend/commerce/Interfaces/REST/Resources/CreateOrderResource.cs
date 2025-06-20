using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateOrderResource(
        [Required] int UserClientId,
        [Required] [EmailAddress] string UserEmail,
        [Required] [Phone] string UserPhone,
        [Required] [StringLength(255)] string UserFullName,
        [Required] bool IsDelivery,
        ShippingResource ShippingDetails, 
        [Required] List<int> CartItemIds 
    );
}