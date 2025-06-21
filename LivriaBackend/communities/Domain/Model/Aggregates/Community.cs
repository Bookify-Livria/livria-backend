using System.Collections.Generic;
using LivriaBackend.users.Domain.Model.Aggregates; 
using System.Linq; 

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    /// <summary>
    /// Representa un agregado de comunidad dentro del dominio.
    /// Una comunidad agrupa publicaciones y usuarios que la integran.
    /// </summary>
    public class Community
    {
        /// <summary>
        /// Obtiene el identificador único de la comunidad.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Obtiene el nombre de la comunidad.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Obtiene la descripción de la comunidad.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Obtiene el tipo de comunidad (por ejemplo, "pública", "privada").
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Obtiene la URL de la imagen de perfil de la comunidad.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// Obtiene la URL de la imagen de banner de la comunidad.
        /// </summary>
        public string Banner { get; private set; }

        /// <summary>
        /// Obtiene una colección de las publicaciones asociadas a esta comunidad.
        /// </summary>
        public ICollection<Post> Posts { get; private set; } = new List<Post>(); 

        /// <summary>
        /// Obtiene una colección de las relaciones entre usuarios y esta comunidad (miembros).
        /// </summary>
        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();

        /// <summary>
        /// Constructor privado sin parámetros para uso de Entity Framework Core.
        /// Inicializa las colecciones de publicaciones y relaciones de usuarios con la comunidad.
        /// </summary>
        private Community()
        {
            Posts = new List<Post>(); 
            UserCommunities = new List<UserCommunity>(); 
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Community"/> con los detalles especificados.
        /// </summary>
        /// <param name="name">El nombre de la comunidad.</param>
        /// <param name="description">La descripción de la comunidad.</param>
        /// <param name="type">El tipo de comunidad (pública, privada, etc.).</param>
        /// <param name="image">La URL de la imagen de perfil de la comunidad.</param>
        /// <param name="banner">La URL del banner de la comunidad.</param>
        public Community(string name, string description, string type, string image, string banner)
        {
            Name = name;
            Description = description;
            Type = type;
            Image = image;
            Banner = banner;
            Posts = new List<Post>(); 
            UserCommunities = new List<UserCommunity>(); 
        }

        /// <summary>
        /// Añade una nueva publicación a la colección de publicaciones de la comunidad.
        /// </summary>
        /// <param name="post">La publicación a añadir. Debe ser un objeto <see cref="Post"/> válido y no debe estar ya en la colección.</param>
        public void AddPost(Post post)
        {
            if (post != null && !Posts.Contains(post))
            {
                Posts.Add(post);
            }
        }

        /// <summary>
        /// Añade un cliente de usuario como miembro de esta comunidad creando una relación <see cref="UserCommunity"/>.
        /// </summary>
        /// <param name="userClient">El cliente de usuario a añadir. Debe ser un objeto <see cref="UserClient"/> válido y no debe ser ya miembro de la comunidad.</param>
        public void AddUser(UserClient userClient)
        {
            if (userClient != null && !UserCommunities.Any(uc => uc.UserClientId == userClient.Id))
            {
                UserCommunities.Add(new UserCommunity(userClient.Id, this.Id));
            }
        }
    }
}