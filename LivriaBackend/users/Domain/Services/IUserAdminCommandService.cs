using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    /// <summary>
    /// Define el contrato para el servicio de comandos de administradores de usuario.
    /// Este servicio es responsable de orquestar las operaciones que modifican el estado de los administradores de usuario.
    /// </summary>
    public interface IUserAdminCommandService
    {
        /// <summary>
        /// Maneja el comando para actualizar un administrador de usuario existente.
        /// </summary>
        /// <param name="command">El comando <see cref="UpdateUserAdminCommand"/> que contiene los datos actualizados del administrador de usuario.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es el objeto <see cref="UserAdmin"/> actualizado.
        /// </returns>
        Task<UserAdmin> Handle(UpdateUserAdminCommand command);
    }
}