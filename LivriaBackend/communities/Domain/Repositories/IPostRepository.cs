using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; 

namespace LivriaBackend.communities.Domain.Repositories
{

    public interface IPostRepository : IAsyncRepository<Post>
    {

    }
}