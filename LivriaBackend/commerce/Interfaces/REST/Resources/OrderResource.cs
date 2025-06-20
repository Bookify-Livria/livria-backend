using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System; 

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record OrderResource(
        int Id,
        
        string Code,
        
        int UserClientId,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserEmail,
        
        [Phone(ErrorMessage = "PhoneError")]
        string UserPhone,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string UserFullName,
        
        bool IsDelivery,
        
        ShippingResource Shipping, 
        
        decimal Total,
        
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/1/1900", "12/12/3000", ErrorMessage = "DateOutOfRange")]
        DateTime Date, 
        
        IEnumerable<OrderItemResource> Items
    );
}