using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivriaBackend.commerce.Interfaces.REST.Resources; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record UserClientResource(
        int Id,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Email,
        
        /* [Url(ErrorMessage = "UrlError")] */
        string Icon,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Phrase,
        
        List<OrderResource> Orders, 
        
        string Subscription
        
    ) : UserResource(Id, Display, Username, Email); 
}