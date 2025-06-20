using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Repositories
{
    public interface IUserClientRepository
    {
        Task<UserClient> GetByIdAsync(int id);
        Task<IEnumerable<UserClient>> GetAllAsync();
        Task<UserClient> GetByUsernameAsync(string username); // ¡Nuevo método clave!
        Task AddAsync(UserClient userClient);
        Task UpdateAsync(UserClient userClient);
        Task DeleteAsync(int id);
    }
}