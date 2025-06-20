using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; 
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.users.Infrastructure.Repositories
{
    public class UserAdminRepository : IUserAdminRepository
    {
        private readonly AppDbContext _context;

        public UserAdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserAdmin> GetByIdAsync(int id)
        {
            return await _context.UserAdmins.FirstOrDefaultAsync(ua => ua.Id == id);
        }

        public async Task<IEnumerable<UserAdmin>> GetAllAsync()
        {
            return await _context.UserAdmins.ToListAsync();
        }

        public async Task AddAsync(UserAdmin userAdmin)
        {
            await _context.UserAdmins.AddAsync(userAdmin);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(UserAdmin userAdmin)
        {
            _context.UserAdmins.Update(userAdmin);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var userAdminToDelete = await _context.UserAdmins.FirstOrDefaultAsync(ua => ua.Id == id);
            if (userAdminToDelete != null)
            {
                _context.UserAdmins.Remove(userAdminToDelete);
                await _context.SaveChangesAsync(); 
            }
        }
    }
}