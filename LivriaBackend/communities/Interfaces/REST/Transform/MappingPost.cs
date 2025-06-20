using AutoMapper;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Interfaces.REST.Resources;

namespace LivriaBackend.communities.Interfaces.REST.Transform
{
    public class MappingPost : Profile
    {
        public MappingPost()
        {
            // Mapeo del recurso de entrada a comando
            CreateMap<CreatePostResource, CreatePostCommand>();

            // Mapeo de la entidad Post a PostResource para la respuesta
            CreateMap<Post, PostResource>();
        }
    }
}