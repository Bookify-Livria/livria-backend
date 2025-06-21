using LivriaBackend.communities.Domain.Model.Queries;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.QueryServices
{
    /// <summary>
    /// Implementa el servicio de consulta para las operaciones de la entidad <see cref="Community"/>.
    /// Encapsula la lógica de negocio para recuperar datos de comunidades.
    /// </summary>
    public class CommunityQueryService : ICommunityQueryService
    {
        private readonly ICommunityRepository _communityRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CommunityQueryService"/>.
        /// </summary>
        /// <param name="communityRepository">El repositorio para las operaciones de datos de la comunidad.</param>
        public CommunityQueryService(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        /// <summary>
        /// Maneja el comando para obtener todas las comunidades.
        /// </summary>
        /// <param name="query">La consulta para obtener todas las comunidades.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea es una colección de todas las <see cref="Community"/>.</returns>
        public async Task<IEnumerable<Community>> Handle(GetAllCommunitiesQuery query)
        {
            return await _communityRepository.ListAsync();
        }

        /// <summary>
        /// Maneja el comando para obtener una comunidad por su identificador único.
        /// </summary>
        /// <param name="query">La consulta para obtener una comunidad por ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea es la <see cref="Community"/> encontrada, o null si no existe.</returns>
        public async Task<Community> Handle(GetCommunityByIdQuery query)
        {
            return await _communityRepository.GetByIdAsync(query.CommunityId);
        }
    }
}