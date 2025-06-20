using LivriaBackend.users.Domain.Model.Aggregates; // Para UserClient
using System;

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    public class Post
    {
        public int Id { get; private set; }
        public int CommunityId { get; private set; } // FK a la comunidad
        public int UserId { get; private set; }      // FK al UserClient que hizo el post (es el Id de la tabla base User)

        // El username del autor del post (denormalizado, es bueno para consultas rápidas)
        public string Username { get; private set; }

        public string Content { get; private set; }
        public string Img { get; private set; }
        public DateTime CreatedAt { get; private set; } // Para registrar la fecha de creación

        // Propiedades de navegación de EF Core (opcionales, pero recomendadas)
        public Community Community { get; private set; }
        public UserClient UserClient { get; private set; } // Navegación al UserClient (el autor)

        // Constructor protegido para EF Core
        protected Post() { }

        // Constructor para crear un Post
        public Post(int communityId, int userId, string username, string content, string img)
        {
            CommunityId = communityId;
            UserId = userId;
            Username = username;
            Content = content;
            Img = img;
            CreatedAt = DateTime.UtcNow; // O la zona horaria que prefieras
        }

        // Método para actualizar el contenido del post (ej. edición)
        public void Update(string content, string img)
        {
            Content = content;
            Img = img;
        }
    }
}