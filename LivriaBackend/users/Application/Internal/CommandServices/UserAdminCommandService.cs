using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    public class UserAdminCommandService : IUserAdminCommandService
    {
        private readonly IUserAdminRepository _userAdminRepository;

        public UserAdminCommandService(IUserAdminRepository userAdminRepository)
        {
            _userAdminRepository = userAdminRepository;
        }

        public async Task<UserAdmin> Handle(UpdateUserAdminCommand command)
        {
            var userAdmin = await _userAdminRepository.GetByIdAsync(command.UserAdminId);

            if (userAdmin == null)
            {
                // Manejar error: UserAdmin no encontrado
                throw new ApplicationException($"UserAdmin with Id {command.UserAdminId} not found.");
            }

            userAdmin.Update(command.Display, command.Username, command.Email, command.Password,
                command.AdminAccess, command.SecurityPin);

            await _userAdminRepository.UpdateAsync(userAdmin);
            return userAdmin;
        }

        // Si tuvieras un CreateUserAdminCommand, se implementaría aquí:
        /*
        public async Task<UserAdmin> Handle(CreateUserAdminCommand command)
        {
            var userAdmin = new UserAdmin(command.Display, command.Username, command.Email, command.Password,
                                          command.AdminAccess, command.SecurityPin);
            await _userAdminRepository.AddAsync(userAdmin);
            return userAdmin;
        }
        */
    }
}