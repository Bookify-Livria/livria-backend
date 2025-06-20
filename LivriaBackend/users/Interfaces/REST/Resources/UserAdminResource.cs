using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using LivriaBackend.users.Application.Resources;
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UserAdminResource(
        int Id,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
<<<<<<< HEAD
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
=======
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string Email,
        
        bool AdminAccess,
        
<<<<<<< HEAD
=======
        [StringLength(255, ErrorMessage = "MaxLengthError")]
>>>>>>> 7e68f3afcd0d91417be42b8698d95f516548843d
        string SecurityPin 
    ) : UserResource(Id, Display, Username, Email); 
}