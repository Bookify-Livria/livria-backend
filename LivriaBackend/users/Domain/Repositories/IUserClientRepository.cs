using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Repositories
{
    public interface IUserClientRepository
    {
        // CAMBIO CRÍTICO: Ahora devuelve Task<UserClient?> para manejar casos donde no se encuentra el usuario
        Task<UserClient?> GetByIdAsync(int id); 
        Task<IEnumerable<UserClient>> GetAllAsync();
        Task AddAsync(UserClient userClient);
        Task UpdateAsync(UserClient userClient);
        Task DeleteAsync(int id);
    }
}