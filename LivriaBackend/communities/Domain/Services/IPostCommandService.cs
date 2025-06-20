using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.communities.Domain.Model.Services
{
    public interface IPostCommandService
    {
        Task<Post> Handle(CreatePostCommand command);
        // Podrías añadir métodos para Update y Delete si son necesarios en el futuro
        // Task<Post> Handle(UpdatePostCommand command);
        // Task Delete(DeletePostCommand command);
    }
}