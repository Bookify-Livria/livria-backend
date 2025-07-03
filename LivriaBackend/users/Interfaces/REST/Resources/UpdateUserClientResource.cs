﻿using System.ComponentModel.DataAnnotations; 

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record class UpdateUserClientResource : UpdateUserResource
    {
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Icon { get; init; }
        
        [StringLength(255, ErrorMessage = "MaxLengthError")]
        public string? Phrase { get; init; }
        

        
        public UpdateUserClientResource(
            string? display,
            string? username,
            string? email,
            string? icon,
            string? phrase
        ) : base(display, username, email, icon, phrase)
        {
            Icon = icon;
            Phrase = phrase;
        }
        
        
        public UpdateUserClientResource() : base() { }
    }
}