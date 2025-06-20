using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; // Assuming you have a base IAsyncRepository

namespace LivriaBackend.communities.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Post entity.
    /// Inherits from IAsyncRepository for basic CRUD operations.
    /// </summary>
    public interface IPostRepository : IAsyncRepository<Post>
    {
        // You can add specific methods if needed,
        // e.g., GetPostsByCommunityId
        // Task<IEnumerable<Post>> GetPostsByCommunityIdAsync(int communityId);
    }
}