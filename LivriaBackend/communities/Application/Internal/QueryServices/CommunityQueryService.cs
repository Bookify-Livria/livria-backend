using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.QueryServices
{

    public class CommunityQueryService : ICommunityQueryService
    {
        private readonly ICommunityRepository _communityRepository;

        public CommunityQueryService(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<IEnumerable<Community>> Handle(GetAllCommunitiesQuery query)
        {
            return await _communityRepository.ListAsync();
        }

        public async Task<Community> Handle(GetCommunityByIdQuery query)
        {
            return await _communityRepository.GetByIdAsync(query.CommunityId);
        }
    }
}