using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories; // For ICommunityRepository
using LivriaBackend.users.Domain.Model.Repositories; // For IUserClientRepository
using LivriaBackend.Shared.Domain.Repositories; // For IUnitOfWork
using System;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.CommandServices
{
    /// <summary>
    /// Implements the IUserCommunityCommandService for handling User-Community relationship commands.
    /// </summary>
    public class UserCommunityCommandService : IUserCommunityCommandService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserClientRepository _userClientRepository;
        private readonly IUserCommunityRepository _userCommunityRepository; // NEW
        private readonly IUnitOfWork _unitOfWork;

        public UserCommunityCommandService(
            ICommunityRepository communityRepository,
            IUserClientRepository userClientRepository,
            IUserCommunityRepository userCommunityRepository,
            IUnitOfWork unitOfWork)
        {
            _communityRepository = communityRepository;
            _userClientRepository = userClientRepository;
            _userCommunityRepository = userCommunityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserCommunity> Handle(JoinCommunityCommand command)
        {
            // 1. Validate if UserClient exists
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ApplicationException($"UserClient with ID {command.UserClientId} not found.");
            }

            // 2. Validate if Community exists
            var community = await _communityRepository.GetByIdAsync(command.CommunityId);
            if (community == null)
            {
                throw new ApplicationException($"Community with ID {command.CommunityId} not found.");
            }

            // 3. Check if relationship already exists (user already joined)
            var existingMembership = await _userCommunityRepository.GetByUserAndCommunityIdsAsync(command.UserClientId, command.CommunityId);
            if (existingMembership != null)
            {
                throw new ApplicationException($"UserClient {command.UserClientId} is already a member of Community {command.CommunityId}.");
            }

            // 4. Create the UserCommunity entry
            var userCommunity = new UserCommunity(command.UserClientId, command.CommunityId);
            await _userCommunityRepository.AddAsync(userCommunity);
            await _unitOfWork.CompleteAsync();

            return userCommunity;
        }
    }
}