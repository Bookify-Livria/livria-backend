using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore; // Add this using directive
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Infrastructure.Repositories
{

    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Community> GetByIdAsync(int id)
        {
            return await Context.Communities
                .Include(c => c.Posts) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Community>> ListAsync()
        {
            return await Context.Communities
                .Include(c => c.Posts)
                .ToListAsync();
        }
    }
}