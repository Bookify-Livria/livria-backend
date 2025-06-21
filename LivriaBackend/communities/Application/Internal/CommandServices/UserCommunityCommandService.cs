using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories; 
using LivriaBackend.users.Domain.Model.Repositories; 
using LivriaBackend.Shared.Domain.Repositories; 
using System;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.CommandServices
{
    /// <summary>
    /// Implementa el servicio de comandos para las operaciones de la entidad agregada <see cref="UserCommunity"/>.
    /// Encapsula la lógica de negocio para que los usuarios se unan a las comunidades.
    /// </summary>
    public class UserCommunityCommandService : IUserCommunityCommandService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserClientRepository _userClientRepository;
        private readonly IUserCommunityRepository _userCommunityRepository; 
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserCommunityCommandService"/>.
        /// </summary>
        /// <param name="communityRepository">El repositorio para las operaciones de datos de la comunidad.</param>
        /// <param name="userClientRepository">El repositorio para las operaciones de datos del cliente de usuario.</param>
        /// <param name="userCommunityRepository">El repositorio para las operaciones de datos de las relaciones usuario-comunidad.</param>
        /// <param name="unitOfWork">La unidad de trabajo para gestionar las transacciones de base de datos.</param>
        public UserCommunityCommandService(
            ICommunityRepository communityRepository,
            IUserClientRepository userClientRepository,
            IUserCommunityRepository userCommunityRepository,
            IUnitOfWork unitOfWork)
        {
            _communityRepository = communityRepository;
            _userClientRepository = userClientRepository;
            _userCommunityRepository = userCommunityRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja el comando para que un usuario se una a una comunidad.
        /// Realiza validaciones para asegurar la existencia del usuario y la comunidad, el tipo de suscripción del usuario,
        /// y para evitar que un usuario se una a la misma comunidad múltiples veces.
        /// </summary>
        /// <param name="command">El comando que contiene los IDs del usuario y la comunidad para la unión.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea es el objeto <see cref="UserCommunity"/> creado.</returns>
        /// <exception cref="ApplicationException">
        /// Se lanza si el cliente de usuario o la comunidad no se encuentran,
        /// si el usuario no tiene la suscripción 'communityplan',
        /// o si el usuario ya es miembro de la comunidad.
        /// </exception>
        public async Task<UserCommunity> Handle(JoinCommunityCommand command)
        {
            
            var userClient = await _userClientRepository.GetByIdAsync(command.UserClientId);
            if (userClient == null)
            {
                throw new ApplicationException($"UserClient with ID {command.UserClientId} not found.");
            }

            
            if (userClient.Subscription != "communityplan")
            {
                throw new ApplicationException("User must have a 'communityplan' subscription to join a community.");
            }
            
            
            var community = await _communityRepository.GetByIdAsync(command.CommunityId);
            if (community == null)
            {
                throw new ApplicationException($"Community with ID {command.CommunityId} not found.");
            }

            
            var existingMembership = await _userCommunityRepository.GetByUserAndCommunityIdsAsync(command.UserClientId, command.CommunityId);
            if (existingMembership != null)
            {
                throw new ApplicationException($"UserClient {command.UserClientId} is already a member of Community {command.CommunityId}.");
            }

            
            var userCommunity = new UserCommunity(command.UserClientId, command.CommunityId);
            
            await _userCommunityRepository.AddAsync(userCommunity);
            
            await _unitOfWork.CompleteAsync();

            return userCommunity;
        }
    }
}