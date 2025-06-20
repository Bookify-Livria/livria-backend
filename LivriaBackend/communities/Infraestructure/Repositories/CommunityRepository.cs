using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore; // Add this using directive
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Infrastructure.Repositories
{
    /// <summary>
    /// EF Core implementation of ICommunityRepository.
    /// </summary>
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(AppDbContext context) : base(context)
        {
        }

        // Override GetByIdAsync to include Posts
        public override async Task<Community> GetByIdAsync(int id)
        {
            return await Context.Communities
                .Include(c => c.Posts) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Override ListAsync to include Posts
        public override async Task<IEnumerable<Community>> ListAsync()
        {
            return await Context.Communities
                .Include(c => c.Posts)
                .ToListAsync();
        }
    }
}