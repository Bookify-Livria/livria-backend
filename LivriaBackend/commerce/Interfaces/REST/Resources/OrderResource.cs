using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System;
using LivriaBackend.users.Application.Resources;
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record OrderResource(
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
        int Id,
        
<<<<<<< HEAD
        string Code,
        
=======
        [StringLength(6, ErrorMessage = "MaxLengthError")]
        string Code,
        
        [Range(0, int.MaxValue, ErrorMessage = "MinimumValueError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        int UserClientId,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserEmail,
        
<<<<<<< HEAD
        [Phone(ErrorMessage = "PhoneError")]
=======
        [Phone(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "PhoneError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string UserPhone,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserFullName,
        
        bool IsDelivery,
        
        ShippingResource Shipping, 
        
        decimal Total,
        
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/1/1900", "12/12/3000", ErrorMessage = "DateOutOfRange")]
<<<<<<< HEAD
        string Date, 
=======
        DateTime Date, 
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        
        IEnumerable<OrderItemResource> Items
    );
}