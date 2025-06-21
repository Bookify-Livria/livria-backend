using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.Shared.Domain.Repositories; 

namespace LivriaBackend.communities.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de repositorio para el agregado <see cref="Community"/>.
    /// Hereda las operaciones CRUD asíncronas básicas de <see cref="IAsyncRepository{T}"/>.
    /// </summary>
    public interface ICommunityRepository : IAsyncRepository<Community>
    {
        
    }
}