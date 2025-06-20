using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Repositories
{
    public interface IUserClientRepository
    {
        Task<UserClient> GetByIdAsync(int id);
        Task<IEnumerable<UserClient>> GetAllAsync();
        Task<UserClient> GetByUsernameAsync(string username); 
        Task AddAsync(UserClient userClient);
        Task UpdateAsync(UserClient userClient);
        Task DeleteAsync(UserClient userClient);
        Task<UserClient> GetByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
    }
}