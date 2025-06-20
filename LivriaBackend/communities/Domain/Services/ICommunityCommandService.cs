using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{
    /// <summary>
    /// Service interface for handling Community commands.
    /// </summary>
    public interface ICommunityCommandService
    {
        Task<Community> Handle(CreateCommunityCommand command);
        // No Handle for Update/Delete based on your requirements
    }
}