using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Repositories
{
    public interface IUserAdminRepository
    {
        Task<UserAdmin> GetByIdAsync(int id);
        Task<IEnumerable<UserAdmin>> GetAllAsync();

        
        
        Task AddAsync(UserAdmin userAdmin);
        Task UpdateAsync(UserAdmin userAdmin); 
        Task DeleteAsync(int id); 
    }
}