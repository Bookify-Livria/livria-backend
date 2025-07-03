using System.Threading.Tasks;
using System;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;

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
        /// El resultado de la tarea es el objeto <see cref="UserAdmin"/> actualizado, o null si no se encuentra.
        /// </returns>
        public async Task<UserAdmin?> Handle(UpdateUserAdminCommand command)
        {
            var userAdmin = await _userAdminRepository.GetByIdAsync(command.UserAdminId);

            if (userAdmin == null)
            {
                return null;
            }

            userAdmin.Update(
                command.Display,
                command.Username,
                command.Email,
                command.AdminAccess,
                command.SecurityPin
            );

            await _userAdminRepository.UpdateAsync(userAdmin);
            await _unitOfWork.CompleteAsync();
            return userAdmin;
        }

        /// <summary>
        /// Actualiza el capital de un UserAdmin específico.
        /// </summary>
        /// <param name="userAdminId">El ID del UserAdmin a actualizar.</param>
        /// <param name="amountToAdd">La cantidad a añadir/restar al capital. Puede ser positiva para añadir, negativa para restar.</param>
        /// <returns>El objeto UserAdmin actualizado, o null si no se encuentra.</returns>
        public async Task<UserAdmin?> UpdateUserAdminCapitalAsync(int userAdminId, decimal amountToAdd)
        {
            var userAdmin = await _userAdminRepository.GetByIdAsync(userAdminId);
            if (userAdmin == null)
            {
                // Es importante lanzar una excepción o manejar este caso adecuadamente
                // Si llegamos aquí, significa que el UserAdmin con ese ID no existe.
                throw new InvalidOperationException($"UserAdmin with ID {userAdminId} not found for capital update.");
            }

            // Decide si añadir o restar capital basándose en el signo de amountToAdd
            if (amountToAdd >= 0)
            {
                userAdmin.AddCapital(amountToAdd);
            }
            else
            {
                // Si amountToAdd es negativo, pasamos su valor absoluto a DecreaseCapital
                userAdmin.DecreaseCapital(Math.Abs(amountToAdd));
            }
            
            await _userAdminRepository.UpdateAsync(userAdmin); // Asegúrate de que el repositorio actualice la entidad
            await _unitOfWork.CompleteAsync();
            return userAdmin;
        }
    }
}