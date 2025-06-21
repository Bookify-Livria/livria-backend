using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.Shared.Domain.Repositories; 
using System.Threading.Tasks;

namespace LivriaBackend.communities.Application.Internal.CommandServices
{
    /// <summary>
    /// Implementa el servicio de comandos para las operaciones de la entidad <see cref="Community"/>.
    /// Encapsula la lógica de negocio para la creación y gestión de comunidades.
    /// </summary>
    public class CommunityCommandService : ICommunityCommandService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CommunityCommandService"/>.
        /// </summary>
        /// <param name="communityRepository">El repositorio para las operaciones de datos de la comunidad.</param>
        /// <param name="unitOfWork">La unidad de trabajo para gestionar las transacciones de base de datos.</param>
        public CommunityCommandService(ICommunityRepository communityRepository, IUnitOfWork unitOfWork)
        {
            _communityRepository = communityRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja el comando para crear una nueva comunidad.
        /// </summary>
        /// <param name="command">El comando que contiene los datos para crear la comunidad.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea es la <see cref="Community"/> recién creada.</returns>
        public async Task<Community> Handle(CreateCommunityCommand command)
        {
            var community = new Community(command.Name, command.Description, command.Type, command.Image, command.Banner);
            
            await _communityRepository.AddAsync(community);
            
            await _unitOfWork.CompleteAsync();

            return community;
        }
    }
}