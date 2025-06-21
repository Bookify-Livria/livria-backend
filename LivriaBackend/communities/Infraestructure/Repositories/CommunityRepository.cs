using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Infrastructure.Repositories
{
    /// <summary>
    /// Implementa el repositorio para la entidad agregada <see cref="Community"/>.
    /// Proporciona métodos para la persistencia y recuperación de datos de comunidades,
    /// incluyendo la carga de relaciones.
    /// </summary>
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CommunityRepository"/>.
        /// </summary>
        /// <param name="context">El contexto de la base de datos de la aplicación.</param>
        public CommunityRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Obtiene una comunidad por su identificador único, incluyendo sus publicaciones asociadas.
        /// </summary>
        /// <param name="id">El identificador único de la comunidad.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es la <see cref="Community"/> encontrada, o <c>null</c> si no existe.
        /// Las publicaciones (<see cref="Community.Posts"/>) se cargan de forma ansiosa.
        /// </returns>
        public override async Task<Community> GetByIdAsync(int id)
        {
            return await Context.Communities
                .Include(c => c.Posts) // Carga ansiosa de las publicaciones de la comunidad
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Obtiene una colección de todas las comunidades, incluyendo sus publicaciones asociadas.
        /// </summary>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es una colección de todas las <see cref="Community"/>,
        /// con sus publicaciones (<see cref="Community.Posts"/>) cargadas de forma ansiosa.
        /// </returns>
        public override async Task<IEnumerable<Community>> ListAsync()
        {
            return await Context.Communities
                .Include(c => c.Posts) 
                .ToListAsync();
        }
    }
}