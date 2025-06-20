using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    public abstract class User
    {
        public int Id { get; protected set; } 
        public string Display { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } 

        protected User() { }

        
        public User(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }

        
        protected User(int id, string display, string username, string email, string password)
            : this(display, username, email, password) // Llama al constructor principal
        {
            Id = id; // Asigna el Id explícitamente
        }

        
        protected void UpdateUserProperties(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}