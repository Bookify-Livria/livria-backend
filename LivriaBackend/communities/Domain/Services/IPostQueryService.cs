using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{
    /// <summary>
    /// Service interface for handling Post queries.
    /// </summary>
    public interface IPostQueryService
    {
        Task<IEnumerable<Post>> Handle(GetAllPostsQuery query);
        Task<Post> Handle(GetPostByIdQuery query);
    }
}