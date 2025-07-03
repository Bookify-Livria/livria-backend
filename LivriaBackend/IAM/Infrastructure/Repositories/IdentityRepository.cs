using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivriaBackend.IAM.Domain.Model.Aggregates;
using LivriaBackend.IAM.Domain.Repositories;
// Quita el using de IAM.Infrastructure.Persistence.Contexts si lo tenías
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; // <-- ¡Nuevo! Para AppDbContext

namespace LivriaBackend.IAM.Infrastructure.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly AppDbContext _context; // <-- ¡Cambiado de IAMDbContext a AppDbContext!

        public IdentityRepository(AppDbContext context) // <-- ¡Cambiado el tipo del constructor!
        {
            _context = context;
        }

        public async Task AddAsync(Identity identity)
        {
            await _context.Identities.AddAsync(identity);
        }

        public async Task<Identity> GetByIdAsync(int id)
        {
            return await _context.Identities
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Identities.AnyAsync(i => i.UserName == username);
        }
        public async Task<Identity> GetByUsernameAsync(string username)
        {
            return await _context.Identities
                .FirstOrDefaultAsync(i => i.UserName == username);
        }

        public Task UpdateAsync(Identity identity)
        {
            _context.Entry(identity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}