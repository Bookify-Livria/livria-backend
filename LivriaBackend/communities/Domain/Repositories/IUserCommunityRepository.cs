using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Repositories
{
    /// <summary>
    /// Repository interface for UserCommunity entity.
    /// </summary>
    public interface IUserCommunityRepository : IAsyncRepository<UserCommunity>
    {
        // Specific method to check for existing membership by composite key
        Task<UserCommunity> GetByUserAndCommunityIdsAsync(int userClientId, int communityId);
    }
}