using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; 
using System.Collections.Generic; 
using System.Threading.Tasks;     

namespace LivriaBackend.communities.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de repositorio para la entidad <see cref="Post"/>.
    /// Hereda las operaciones CRUD asíncronas básicas de <see cref="IAsyncRepository{T}"/>
    /// y añade métodos específicos para consultas de publicaciones.
    /// </summary>
    public interface IPostRepository : IAsyncRepository<Post>
    {
        /// <summary>
        /// Obtiene una colección de todas las publicaciones que pertenecen a una comunidad específica.
        /// </summary>
        /// <param name="communityId">El identificador único de la comunidad.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. 
        /// El resultado de la tarea es una colección de <see cref="Post"/> asociadas a la comunidad especificada.
        /// Retorna una colección vacía si no se encuentran publicaciones para el ID de comunidad dado.
        /// </returns>
        Task<IEnumerable<Post>> GetByCommunityIdAsync(int communityId);
    }
}