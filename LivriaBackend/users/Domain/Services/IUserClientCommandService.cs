using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Domain.Model.Aggregates; 
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