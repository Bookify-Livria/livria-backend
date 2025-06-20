using LivriaBackend.commerce.Domain.Model.ValueObjects; // Para Shipping (en el comando)
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.Commands
{
    public record CreateOrderCommand(
        [Required] int UserClientId,
        [Required] [EmailAddress] string UserEmail,
        [Required] [Phone] string UserPhone,
        [Required] [StringLength(255)] string UserFullName,
        [Required] bool IsDelivery,
        Shipping ShippingDetails 
        , [Required] List<int> CartItemIds 
    );
}