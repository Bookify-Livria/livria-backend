using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories; // Para IUnitOfWork
using System.Threading.Tasks;
using LivriaBackend.users.Domain.Model.Repositories; // Para IUserClientRepository
using System; // Para ArgumentException

namespace LivriaBackend.communities.Application.Internal.CommandServices
{
    public class PostCommandService : IPostCommandService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserClientRepository _userClientRepository; // Nuevo: para verificar el UserClient
        private readonly IUnitOfWork _unitOfWork;

        public PostCommandService(
            IPostRepository postRepository,
            ICommunityRepository communityRepository,
            IUserClientRepository userClientRepository, // Inyectar el repositorio del cliente
            IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _communityRepository = communityRepository;
            _userClientRepository = userClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(CreatePostCommand command)
        {
            // 1. Validar que la comunidad existe
            var community = await _communityRepository.GetByIdAsync(command.CommunityId);
            if (community == null)
            {
                throw new ArgumentException($"Community with ID {command.CommunityId} not found.");
            }

            // 2. Validar que el UserClient existe por Username y obtener su ID
            var userClient = await _userClientRepository.GetByUsernameAsync(command.Username);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with Username '{command.Username}' not found.");
            }

            // 3. Crear el nuevo Post
            var post = new Post(command.CommunityId, userClient.Id, command.Username, command.Content, command.Img);

            // 4. Añadir el Post al repositorio
            await _postRepository.AddAsync(post);
            await _unitOfWork.CompleteAsync(); // Guardar cambios en la unidad de trabajo

            return post;
        }
    }
}