using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.Internal.QueryServices
{
    public class UserAdminQueryService : IUserAdminQueryService
    {
        private readonly IUserAdminRepository _userAdminRepository;

        public UserAdminQueryService(IUserAdminRepository userAdminRepository)
        {
            _userAdminRepository = userAdminRepository;
        }

        public async Task<IEnumerable<UserAdmin>> Handle(GetAllUserAdminQuery query)
        {
            return await _userAdminRepository.GetAllAsync();
        }
    }
}