using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; // Assuming you have a base IAsyncRepository

namespace LivriaBackend.communities.Domain.Repositories
{

    public interface IPostRepository : IAsyncRepository<Post>
    {

    }
}