using System;
using System.Collections.Generic;

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    public abstract class User
    {
        public int Id { get; protected set; } // Propiedad Id con setter protegido para que EF Core pueda asignarlo y la reflexión en Program.cs
        public string Display { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } // Considerar hashing para seguridad

        // Constructor protegido para EF Core (sin parámetros o con los mínimos necesarios para la reconstrucción)
        protected User() { }

        // Constructor principal para la creación de usuarios
        public User(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }

        // Constructor protegido para usar con Id si es necesario para el seeding manual directo o casos específicos
        protected User(int id, string display, string username, string email, string password)
            : this(display, username, email, password) // Llama al constructor principal
        {
            Id = id; // Asigna el Id explícitamente
        }

        // Método protegido para actualizar las propiedades de la clase base User
        protected void UpdateUserProperties(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}