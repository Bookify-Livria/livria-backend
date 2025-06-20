using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{
    /// <summary>
    /// Service interface for handling User-Community relationship commands.
    /// </summary>
    public interface IUserCommunityCommandService
    {
        Task<UserCommunity> Handle(JoinCommunityCommand command);
        // Task Handle(LeaveCommunityCommand command); // Add if you want a "leave" functionality
    }
}