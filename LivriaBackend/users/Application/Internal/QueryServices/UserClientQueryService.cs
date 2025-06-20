using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.Internal.QueryServices
{
    public class UserClientQueryService : IUserClientQueryService
    {
        private readonly IUserClientRepository _userClientRepository;

        public UserClientQueryService(IUserClientRepository userClientRepository)
        {
            _userClientRepository = userClientRepository;
        }

        public async Task<IEnumerable<UserClient>> Handle(GetAllUserClientQuery query)
        {
            return await _userClientRepository.GetAllAsync();
        }

        public async Task<UserClient> Handle(GetUserClientByIdQuery query)
        {
            return await _userClientRepository.GetByIdAsync(query.UserClientId);
        }
    }
}