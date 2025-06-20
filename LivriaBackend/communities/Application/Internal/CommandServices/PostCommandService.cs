using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Domain.Repositories;
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories; // Para IUnitOfWork
using System.Threading.Tasks;
using LivriaBackend.users.Domain.Model.Repositories; // Para IUserClientRepository
using System; 

namespace LivriaBackend.communities.Application.Internal.CommandServices
{
    public class PostCommandService : IPostCommandService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserClientRepository _userClientRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public PostCommandService(
            IPostRepository postRepository,
            ICommunityRepository communityRepository,
            IUserClientRepository userClientRepository, 
            IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _communityRepository = communityRepository;
            _userClientRepository = userClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(CreatePostCommand command)
        {
            var community = await _communityRepository.GetByIdAsync(command.CommunityId);
            if (community == null)
            {
                throw new ArgumentException($"Community with ID {command.CommunityId} not found.");
            }

            var userClient = await _userClientRepository.GetByUsernameAsync(command.Username);
            if (userClient == null)
            {
                throw new ArgumentException($"UserClient with Username '{command.Username}' not found.");
            }

            var post = new Post(command.CommunityId, userClient.Id, command.Username, command.Content, command.Img);

            await _postRepository.AddAsync(post);
            await _unitOfWork.CompleteAsync(); 

            return post;
        }
    }
}