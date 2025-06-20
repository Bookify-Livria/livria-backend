using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserAdminQueryService
    {
        Task<IEnumerable<UserAdmin>> Handle(GetAllUserAdminQuery query);
        
    }
}