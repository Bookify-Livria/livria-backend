using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.Shared.Domain.Repositories
{
 
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task AddAsync(TEntity entity);
        Task<bool> ExistsAsync(int id);
    }
}