using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserAdminCommandService
    {
        // Solo permitir la actualización
        Task<UserAdmin> Handle(UpdateUserAdminCommand command);
    }
}