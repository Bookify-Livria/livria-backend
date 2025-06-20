using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UserAdminResource(
        int Id,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        string Email,
        
        bool AdminAccess,
        
        string SecurityPin 
    ) : UserResource(Id, Display, Username, Email); 
}