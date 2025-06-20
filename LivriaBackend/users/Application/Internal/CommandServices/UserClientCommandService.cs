using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    public class UserClientCommandService : IUserClientCommandService
    {
        private readonly IUserClientRepository _userClientRepository;

        public UserClientCommandService(IUserClientRepository userClientRepository)
        {
            _userClientRepository = userClientRepository;
        }

        public async Task<UserClient> Handle(CreateUserClientCommand command)
        {
            var userClient = new UserClient(command.Display, command.Username, command.Email, command.Password,
                                            command.Icon, command.Phrase, command.Subscription);
            await _userClientRepository.AddAsync(userClient);
            return userClient;
        }

        public async Task<UserClient> Handle(UpdateUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);

            if (userClient == null)
            {
                // Manejar error: UserClient no encontrado
                throw new ApplicationException($"UserClient with Id {command.UserClientId} not found.");
            }

            userClient.Update(command.Display, command.Username, command.Email, command.Password,
                              command.Icon, command.Phrase, command.Subscription);

            await _userClientRepository.UpdateAsync(userClient);
            return userClient;
        }

        public async Task Handle(DeleteUserClientCommand command)
        {
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                // Opcional: manejar el caso de que no se encuentre, o simplemente no hacer nada
                // dependiendo de la semántica de tu DELETE.
                throw new ApplicationException($"UserClient with Id {command.UserClientId} not found for deletion.");
            }
            await _userClientRepository.DeleteAsync(command.UserClientId);
        }
    }
}