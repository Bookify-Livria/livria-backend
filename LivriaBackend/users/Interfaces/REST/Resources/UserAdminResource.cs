﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivriaBackend.users.Application.Resources;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record UserAdminResource(
        int Id,
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Display,
        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LengthError")]
        string Username,
        
        [StringLength(100, ErrorMessage = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(DataAnnotations), ErrorMessageResourceName = "EmailError")]
        string Email,
        
        bool AdminAccess,
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        string SecurityPin 
    ) : UserResource(Id, Display, Username, Email); 
}