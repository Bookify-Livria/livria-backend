using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; // For AppDbContext
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Repositories; // For BaseRepository

namespace LivriaBackend.communities.Infrastructure.Repositories
{
    /// <summary>
    /// EF Core implementation of IPostRepository.
    /// </summary>
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
        // Specific methods if added to IPostRepository
    }
}