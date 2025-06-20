using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Threading.Tasks;
using System;
using LivriaBackend.Shared.Domain.Repositories; 

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    public class UserClientCommandService : IUserClientCommandService
    {
        private readonly IUserClientRepository _userClientRepository;
        private readonly IUnitOfWork _unitOfWork; 

        public UserClientCommandService(IUserClientRepository userClientRepository, IUnitOfWork unitOfWork)
        {
            _userClientRepository = userClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserClient> Handle(CreateUserClientCommand command)
        {
            var userClient = new UserClient(command.Display, command.Username, command.Email, command.Password,
                                            command.Icon, command.Phrase, command.Subscription);
            await _userClientRepository.AddAsync(userClient);
            await _unitOfWork.CompleteAsync(); 
            return userClient;
        }

        public async Task<UserClient> Handle(UpdateUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);

            if (userClient == null)
            {
                throw new ApplicationException($"UserClient with Id {command.UserClientId} not found.");
            }

            userClient.Update(command.Display, command.Username, command.Email, command.Password,
                              command.Icon, command.Phrase, command.Subscription);

            await _userClientRepository.UpdateAsync(userClient);
            await _unitOfWork.CompleteAsync(); 
            return userClient;
        }

        public async Task Handle(DeleteUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ApplicationException($"UserClient with Id {command.UserClientId} not found for deletion.");
            }
            await _userClientRepository.DeleteAsync(command.UserClientId);
            await _unitOfWork.CompleteAsync(); 
        }
    }
}