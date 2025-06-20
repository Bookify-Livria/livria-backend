using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.Shared.Domain.Repositories
{
    /// <summary>
    /// Generic asynchronous repository interface for common data operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task AddAsync(TEntity entity);
        // Task UpdateAsync(TEntity entity); // <-- ¡ELIMINAR O COMENTAR ESTA LÍNEA!
        // Task DeleteAsync(TEntity entity); // <-- ¡ELIMINAR O COMENTAR ESTA LÍNEA!
        Task<bool> ExistsAsync(int id);
    }
}