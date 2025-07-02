using System.Collections.Generic;
using LivriaBackend.users.Domain.Model.Aggregates;
using System.Linq;
using LivriaBackend.communities.Domain.Model.ValueObjects; // ¡Nuevo using!

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    /// <summary>
    /// Representa un agregado de comunidad dentro del dominio.
    /// Una comunidad agrupa usuarios que la integran.
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
        /// Obtiene el tipo de comunidad (género literario).
        /// </summary>
        public CommunityType Type { get; private set; } // ¡Modificado a CommunityType!

        /// <summary>
        /// Obtiene la URL de la imagen de perfil de la comunidad.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// Obtiene la URL de la imagen de banner de la comunidad.
        /// </summary>
        public string Banner { get; private set; }


        /// <summary>
        /// Obtiene una colección de las relaciones entre usuarios y esta comunidad (miembros).
        /// </summary>
        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();

        /// <summary>
        /// Constructor privado sin parámetros para uso de Entity Framework Core.
        /// Inicializa las colecciones de relaciones de usuarios con la comunidad.
        /// </summary>
        private Community()
        {
            UserCommunities = new List<UserCommunity>();
            // Es buena práctica inicializar propiedades no nulas en constructores,
            // incluso si son para EF Core, a menos que sepa que EF Core las poblará.
            Name = string.Empty;
            Description = string.Empty;
            Image = string.Empty;
            Banner = string.Empty;
            // Type será asignado por EF Core al cargar.
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Community"/> con los detalles especificados.
        /// </summary>
        /// <param name="name">El nombre de la comunidad.</param>
        /// <param name="description">La descripción de la comunidad.</param>
        /// <param name="type">El tipo de comunidad (género literario).</param>
        /// <param name="image">La URL de la imagen de perfil de la comunidad. Opcional, por defecto string.Empty.</param>
        /// <param name="banner">La URL del banner de la comunidad. Opcional, por defecto string.Empty.</param>
        public Community(string name, string description, CommunityType type, string? image = null, string? banner = null) // ¡Modificado el tipo de 'type' y hecho 'image' y 'banner' opcionales!
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));

            Name = name;
            Description = description;
            Type = type;
            Image = image ?? string.Empty; // Asigna string.Empty si es nulo
            Banner = banner ?? string.Empty; // Asigna string.Empty si es nulo
            UserCommunities = new List<UserCommunity>();
        }

        // --- Métodos para actualizar la comunidad (si aplica) ---
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name cannot be empty.", nameof(newName));
            Name = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription)) throw new ArgumentException("Description cannot be empty.", nameof(newDescription));
            Description = newDescription;
        }

        public void UpdateType(CommunityType newType) // Método para actualizar el tipo de comunidad
        {
            Type = newType;
        }

        public void UpdateImage(string? newImage)
        {
            Image = newImage ?? string.Empty;
        }

        public void UpdateBanner(string? newBanner)
        {
            Banner = newBanner ?? string.Empty;
        }


        /// <summary>
        /// Añade un cliente de usuario como miembro de esta comunidad creando una relación <see cref="UserCommunity"/>.
        /// </summary>
        /// <param name="userClient">El cliente de usuario a añadir. Debe ser un objeto <see cref="UserClient"/> válido y no debe ser ya miembro de la comunidad.</param>
        public void AddUser(UserClient userClient)
        {
            if (userClient == null)
            {
                throw new ArgumentNullException(nameof(userClient), "UserClient cannot be null.");
            }
            if (UserCommunities.Any(uc => uc.UserClientId == userClient.Id))
            {
                // Podrías lanzar una excepción o simplemente no hacer nada si ya es miembro
                return;
            }
            UserCommunities.Add(new UserCommunity(userClient.Id, this.Id));
        }
    }
}