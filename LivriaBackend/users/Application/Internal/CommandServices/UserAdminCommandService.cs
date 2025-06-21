using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Threading.Tasks;
using System;
using LivriaBackend.Shared.Domain.Repositories; 

namespace LivriaBackend.users.Application.Internal.CommandServices
{
    /// <summary>
    /// Implementa el servicio de comandos para las operaciones de la entidad <see cref="UserAdmin"/>.
    /// Encapsula la lógica de negocio para gestionar los datos de los administradores de usuario.
    /// </summary>
    public class UserAdminCommandService : IUserAdminCommandService
    {
        private readonly IUserAdminRepository _userAdminRepository;
        private readonly IUnitOfWork _unitOfWork; 

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserAdminCommandService"/>.
        /// </summary>
        /// <param name="userAdminRepository">El repositorio para las operaciones de datos del administrador de usuario.</param>
        /// <param name="unitOfWork">La unidad de trabajo para gestionar las transacciones de base de datos.</param>
        public UserAdminCommandService(IUserAdminRepository userAdminRepository, IUnitOfWork unitOfWork)
        {
            _userAdminRepository = userAdminRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja el comando para actualizar un administrador de usuario existente.
        /// </summary>
        /// <param name="command">El comando <see cref="UpdateUserAdminCommand"/> que contiene los datos actualizados del administrador de usuario.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es el objeto <see cref="UserAdmin"/> actualizado.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// Se lanza si el administrador de usuario con el ID especificado no se encuentra en la base de datos.
        /// </exception>
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
            await _unitOfWork.CompleteAsync(); 
            return userAdmin;
        }
    }
}