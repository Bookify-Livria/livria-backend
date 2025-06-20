using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Threading.Tasks;
using System;
using LivriaBackend.Shared.Domain.Repositories; // Asegúrate de tener este using

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    public class UserAdminCommandService : IUserAdminCommandService
    {
        private readonly IUserAdminRepository _userAdminRepository;
        private readonly IUnitOfWork _unitOfWork; // Inyectar IUnitOfWork

        public UserAdminCommandService(IUserAdminRepository userAdminRepository, IUnitOfWork unitOfWork)
        {
            _userAdminRepository = userAdminRepository;
            _unitOfWork = unitOfWork;
        }

        // REMOVIDO: No hay Handle para CreateUserAdminCommand

        public async Task<UserAdmin> Handle(UpdateUserAdminCommand command)
        {
            var userAdmin = await _userAdminRepository.GetByIdAsync(command.UserAdminId);

            if (userAdmin == null)
            {
                throw new ApplicationException($"UserAdmin with Id {command.UserAdminId} not found.");
            }

            userAdmin.Update(command.Display, command.Username, command.Email, command.Password,
                command.AdminAccess, command.SecurityPin);

            await _userAdminRepository.UpdateAsync(userAdmin);
            await _unitOfWork.CompleteAsync(); // Guardar cambios
            return userAdmin;
        }

        // REMOVIDO: No hay Handle para DeleteUserAdminCommand
    }
}