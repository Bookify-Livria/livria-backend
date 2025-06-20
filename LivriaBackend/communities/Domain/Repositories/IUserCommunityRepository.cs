using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Repositories
{

    public interface IUserCommunityRepository : IAsyncRepository<UserCommunity>
    {
        Task<UserCommunity> GetByUserAndCommunityIdsAsync(int userClientId, int communityId);
    }
}