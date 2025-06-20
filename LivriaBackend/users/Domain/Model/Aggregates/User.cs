using System.Text.Json.Serialization; // Puedes quitar esto si no usas System.Text.Json directamente para deserializar a la entidad

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    public abstract class User
    {
        public int Id { get; protected set; } // Correcto para EF Core con Identity

        public string Display { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        protected User(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }

        protected User(int id, string display, string username, string email, string password)
        {
            Id = id;
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }

        protected User() { } // Correcto y necesario para EF Core

        public void Update(string display, string username, string email, string password)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}