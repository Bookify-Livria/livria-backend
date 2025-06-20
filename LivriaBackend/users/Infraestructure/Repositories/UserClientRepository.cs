using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivriaBackend.users.Infrastructure.Repositories
{
    public class UserClientRepository : BaseRepository<UserClient>, IUserClientRepository
    {
        public UserClientRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<UserClient> GetByIdAsync(int id)
        {
            return await this.Context.UserClients
                .Include(uc => uc.UserCommunities)
                .Include(uc => uc.FavoriteBooks)
                .Include(uc => uc.Orders)
                .ThenInclude(o => o.Items)
                .FirstOrDefaultAsync(uc => uc.Id == id);
        }

        public new async Task<IEnumerable<UserClient>> GetAllAsync()
        {
            return await this.Context.UserClients
                .Include(uc => uc.UserCommunities)
                .Include(uc => uc.FavoriteBooks)
                .Include(uc => uc.Orders)
                .ThenInclude(o => o.Items)
                .ToListAsync();
        }

        public async Task AddAsync(UserClient userClient)
        {
            await this.Context.UserClients.AddAsync(userClient);
        }

        public async Task UpdateAsync(UserClient userClient)
        {
            this.Context.Entry(userClient).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(UserClient userClient)
        {
            this.Context.UserClients.Remove(userClient);
            await Task.CompletedTask;
        }

        public async Task<UserClient> GetByUsernameAsync(string username)
        {
            return await this.Context.UserClients
                .Include(uc => uc.UserCommunities)
                .Include(uc => uc.FavoriteBooks)
                .Include(uc => uc.Orders) 
                .ThenInclude(o => o.Items)
                .FirstOrDefaultAsync(uc => uc.Username == username);
        }

        public async Task<UserClient> GetByEmailAsync(string email)
        {
            return await this.Context.UserClients
                .Include(uc => uc.UserCommunities)
                .Include(uc => uc.FavoriteBooks) 
                .Include(uc => uc.Orders) 
                .ThenInclude(o => o.Items)
                .FirstOrDefaultAsync(uc => uc.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await this.Context.UserClients.AnyAsync(uc => uc.Username == username);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await this.Context.UserClients.AnyAsync(uc => uc.Email == email);
        }
    }
}