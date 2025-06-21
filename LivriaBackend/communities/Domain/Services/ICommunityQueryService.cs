using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{

    public interface ICommunityQueryService
    {
        Task<IEnumerable<Community>> Handle(GetAllCommunitiesQuery query);
        Task<Community> Handle(GetCommunityByIdQuery query);
    }
}