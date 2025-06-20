using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; // Tu DbContext está aquí
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivriaBackend.users.Domain.Model.Repositories;

namespace LivriaBackend.users.Infrastructure.Repositories
{
    public class UserClientRepository : IUserClientRepository
    {
        private readonly AppDbContext _context; // Inyecta tu DbContext

        public UserClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserClient userClient)
        {
            await _context.UserClients.AddAsync(userClient); // Añadir a DbSet de UserClients
            await _context.SaveChangesAsync(); // ¡Persistir en la base de datos!
        }

        public async Task<UserClient?> GetByIdAsync(int id) // Puede devolver null si no se encuentra
        {
            // Para TPT, FindAsync suele funcionar, pero si hay problemas con las propiedades base,
            // puedes necesitar un .Include() o .Where().FirstOrDefaultAsync().
            return await _context.UserClients.FindAsync(id);
        }

        public async Task<IEnumerable<UserClient>> GetAllAsync()
        {
            // Obtener todos los UserClients de la base de datos
            return await _context.UserClients.ToListAsync();
        }

        public async Task UpdateAsync(UserClient userClient)
        {
            _context.UserClients.Update(userClient); // Marcar la entidad como modificada
            await _context.SaveChangesAsync(); // Persistir los cambios
        }

        public async Task DeleteAsync(int id)
        {
            var userClient = await _context.UserClients.FindAsync(id);
            if (userClient != null)
            {
                _context.UserClients.Remove(userClient); // Marcar la entidad para eliminación
                await _context.SaveChangesAsync(); // Persistir la eliminación
            }
        }
    }
}