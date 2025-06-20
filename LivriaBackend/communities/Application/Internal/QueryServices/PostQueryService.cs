using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.QueryServices
{
    /// <summary>
    /// Implements the IPostQueryService for handling Post queries.
    /// </summary>
    public class PostQueryService : IPostQueryService
    {
        private readonly IPostRepository _postRepository;

        public PostQueryService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> Handle(GetAllPostsQuery query)
        {
            return await _postRepository.ListAsync();
        }

        public async Task<Post> Handle(GetPostByIdQuery query)
        {
            return await _postRepository.GetByIdAsync(query.PostId);
        }
    }
}