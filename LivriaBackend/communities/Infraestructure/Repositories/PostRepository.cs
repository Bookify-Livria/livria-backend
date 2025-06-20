using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; 
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories; 

namespace LivriaBackend.communities.Infrastructure.Repositories
{

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
    }
}