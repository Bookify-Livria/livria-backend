using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories; 
using LivriaBackend.users.Domain.Model.Repositories; 
using LivriaBackend.Shared.Domain.Repositories; 
using System;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.CommandServices
{

    public class UserCommunityCommandService : IUserCommunityCommandService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserClientRepository _userClientRepository;
        private readonly IUserCommunityRepository _userCommunityRepository; 
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
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ApplicationException($"UserClient with ID {command.UserClientId} not found.");
            }

            if (userClient.Subscription != "communityplan")
            {
                throw new ApplicationException("User must have a 'communityplan' subscription to join a community.");
            }
            
            var community = await _communityRepository.GetByIdAsync(command.CommunityId);
            if (community == null)
            {
                throw new ApplicationException($"Community with ID {command.CommunityId} not found.");
            }

            var existingMembership = await _userCommunityRepository.GetByUserAndCommunityIdsAsync(command.UserClientId, command.CommunityId);
            if (existingMembership != null)
            {
                throw new ApplicationException($"UserClient {command.UserClientId} is already a member of Community {command.CommunityId}.");
            }

            var userCommunity = new UserCommunity(command.UserClientId, command.CommunityId);
            await _userCommunityRepository.AddAsync(userCommunity);
            await _unitOfWork.CompleteAsync();

            return userCommunity;
        }
    }
}