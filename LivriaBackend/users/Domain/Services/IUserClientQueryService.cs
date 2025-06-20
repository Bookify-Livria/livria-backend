using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Domain.Model.Services
{
    public interface IUserClientQueryService
    {
        Task<IEnumerable<UserClient>> Handle(GetAllUserClientQuery query);
        Task<UserClient> Handle(GetUserClientByIdQuery query);
    }
}