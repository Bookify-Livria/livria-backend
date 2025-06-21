using LivriaBackend.users.Domain.Model.Queries;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.users.Application.Internal.QueryServices
{
    /// <summary>
    /// Implementa el servicio de consulta para las operaciones de la entidad <see cref="UserClient"/>.
    /// Encapsula la lógica de negocio para recuperar datos de clientes de usuario.
    /// </summary>
    public class UserClientQueryService : IUserClientQueryService
    {
        private readonly IUserClientRepository _userClientRepository;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserClientQueryService"/>.
        /// </summary>
        /// <param name="userClientRepository">El repositorio para las operaciones de datos del cliente de usuario.</param>
        public UserClientQueryService(IUserClientRepository userClientRepository)
        {
            _userClientRepository = userClientRepository;
        }

        /// <summary>
        /// Maneja la consulta para obtener todos los clientes de usuario en el sistema.
        /// </summary>
        /// <param name="query">La consulta <see cref="GetAllUserClientQuery"/>.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es una colección de todos los objetos <see cref="UserClient"/>.
        /// Retorna una colección vacía si no hay clientes de usuario.
        /// </returns>
        public async Task<IEnumerable<UserClient>> Handle(GetAllUserClientQuery query)
        {
            return await _userClientRepository.GetAllAsync();
        }

        /// <summary>
        /// Maneja la consulta para obtener un cliente de usuario específico por su identificador único.
        /// </summary>
        /// <param name="query">La consulta <see cref="GetUserClientByIdQuery"/> que contiene el ID del cliente de usuario.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// El resultado de la tarea es el objeto <see cref="UserClient"/> encontrado, o <c>null</c> si no existe.
        /// </returns>
        public async Task<UserClient> Handle(GetUserClientByIdQuery query)
        {
            return await _userClientRepository.GetByIdAsync(query.UserClientId);
        }
    }
}