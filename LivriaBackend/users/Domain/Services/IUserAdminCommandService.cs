using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserAdminCommandService
    {
        
        Task<UserAdmin> Handle(UpdateUserAdminCommand command);
    }
}