using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates; // Si los comandos devuelven el agregado actualizado
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserAdminCommandService
    {
        // Task<UserAdmin> Handle(CreateUserAdminCommand command);
        Task<UserAdmin> Handle(UpdateUserAdminCommand command);
        // Task Handle(DeleteUserAdminCommand command); // Si hay un comando de borrar admin
    }
}