using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Repositories
{
    public interface IUserAdminRepository
    {
        Task<UserAdmin> GetByIdAsync(int id);
        Task<IEnumerable<UserAdmin>> GetAllAsync();

        // Estos métodos se mantienen en la interfaz del repositorio,
        // aunque el CommandService no los use, el repositorio los ofrece.
        Task AddAsync(UserAdmin userAdmin);
        Task UpdateAsync(UserAdmin userAdmin); // Debe ser async
        Task DeleteAsync(int id); // Debe ser async
    }
}