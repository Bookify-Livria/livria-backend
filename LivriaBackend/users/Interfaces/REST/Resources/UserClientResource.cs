using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using LivriaBackend.commerce.Interfaces.REST.Resources;
using LivriaBackend.users.Application.Resources;
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record UserClientResource(
        int Id,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
<<<<<<< HEAD
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Email,
        
        /* [Url(ErrorMessage = "UrlError")] */
=======
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        string Email,
        
        [Url(ErrorMessage = "UrlError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Icon,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string Phrase,
        
<<<<<<< HEAD
        List<int> Order, 
        
=======
        List<OrderResource> Orders, 
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Subscription
        
    ) : UserResource(Id, Display, Username, Email); 
}