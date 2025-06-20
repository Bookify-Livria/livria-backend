using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateOrderResource(
        [Required(ErrorMessage = "EmptyField")]
        int UserClientId,
        
        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress]
        string UserEmail,
        
        [Required(ErrorMessage = "EmptyField")]
        [Phone(ErrorMessage = "PhoneError")]
        string UserPhone,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserFullName,
        
        [Required(ErrorMessage = "EmptyField")]
        bool IsDelivery,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        ShippingResource ShippingDetails, 
        
        [Required(ErrorMessage = "EmptyField")]
        List<int> CartItemIds 
    );
}