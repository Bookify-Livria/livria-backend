using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.users.Infrastructure.Repositories
{
    public class UserClientRepository : IUserClientRepository
    {
        private readonly AppDbContext _context;

        public UserClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserClient> GetByIdAsync(int id)
        {
            return await _context.UserClients.FirstOrDefaultAsync(uc => uc.Id == id);
        }

        public async Task<IEnumerable<UserClient>> GetAllAsync()
        {
            return await _context.UserClients.ToListAsync();
        }

        public async Task<UserClient> GetByUsernameAsync(string username) // Implementación
        {
            return await _context.UserClients.FirstOrDefaultAsync(uc => uc.Username == username);
        }

        public async Task AddAsync(UserClient userClient)
        {
            await _context.UserClients.AddAsync(userClient);
            // No llamar SaveChanges aquí si usas UnitOfWork.CompleteAsync() en el servicio de comandos.
        }

        public async Task UpdateAsync(UserClient userClient)
        {
            _context.UserClients.Update(userClient);
            // No llamar SaveChanges aquí si usas UnitOfWork.CompleteAsync() en el servicio de comandos.
        }

        public async Task DeleteAsync(int id)
        {
            var userClientToDelete = await _context.UserClients.FirstOrDefaultAsync(uc => uc.Id == id);
            if (userClientToDelete != null)
            {
                _context.UserClients.Remove(userClientToDelete);
                // No llamar SaveChanges aquí si usas UnitOfWork.CompleteAsync() en el servicio de comandos.
            }
        }
    }
}