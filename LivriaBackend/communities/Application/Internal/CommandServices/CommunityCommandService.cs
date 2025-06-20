using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Domain.Repositories; 
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.CommandServices
{

    public class CommunityCommandService : ICommunityCommandService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommunityCommandService(ICommunityRepository communityRepository, IUnitOfWork unitOfWork)
        {
            _communityRepository = communityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Community> Handle(CreateCommunityCommand command)
        {
            var community = new Community(command.Name, command.Description, command.Type, command.Image, command.Banner);

            await _communityRepository.AddAsync(community);
            await _unitOfWork.CompleteAsync();

            return community;
        }
    }
}