using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivriaBackend.commerce.Application.Resources;

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CreateOrderResource(
        [Required(ErrorMessage = "EmptyField")]
<<<<<<< HEAD
        int UserClientId,
        
        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress]
        string UserEmail,
        
        [Required(ErrorMessage = "EmptyField")]
        [Phone(ErrorMessage = "PhoneError")]
=======
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int UserClientId,
        
        [Required(ErrorMessage = "EmptyField")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        string UserEmail,
        
        [Required(ErrorMessage = "EmptyField")]
        [Phone(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "PhoneError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string UserPhone,
        
        [Required(ErrorMessage = "EmptyField")]
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserFullName,
        
        [Required(ErrorMessage = "EmptyField")]
        bool IsDelivery,
        
<<<<<<< HEAD
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        ShippingResource ShippingDetails, 
=======
        ShippingResource? ShippingDetails, 
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        
        [Required(ErrorMessage = "EmptyField")]
        List<int> CartItemIds 
    );
}