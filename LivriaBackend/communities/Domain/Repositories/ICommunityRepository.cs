using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; // Assuming you have a base IAsyncRepository

namespace LivriaBackend.communities.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Community aggregate.
    /// Inherits from IAsyncRepository for basic CRUD operations.
    /// </summary>
    public interface ICommunityRepository : IAsyncRepository<Community>
    {
        // You can add specific methods here if needed,
        // e.g., GetCommunityByName if it were a common domain operation.
        // Task<Community> GetCommunityByNameAsync(string name);
    }
}