using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{

    public interface IUserCommunityCommandService
    {
        Task<UserCommunity> Handle(JoinCommunityCommand command);
        
    }
}