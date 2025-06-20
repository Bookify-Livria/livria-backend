using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates; // Si los comandos devuelven el agregado actualizado
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserClientCommandService
    {
        Task<UserClient> Handle(CreateUserClientCommand command);
        Task<UserClient> Handle(UpdateUserClientCommand command);
        Task Handle(DeleteUserClientCommand command);
    }
}