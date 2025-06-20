using AutoMapper;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Interfaces.REST.Resources;

namespace LivriaBackend.communities.Interfaces.REST.Transform
{

    public class CommunitiesMappingProfile : Profile
    {
        public CommunitiesMappingProfile()
        {
            
            CreateMap<CreateCommunityResource, CreateCommunityCommand>();
            CreateMap<CreatePostResource, CreatePostCommand>();

            
            CreateMap<Community, CommunityResource>();
            CreateMap<Post, PostResource>();

            
            CreateMap<JoinCommunityResource, JoinCommunityCommand>();
            CreateMap<UserCommunity, UserCommunityResource>();
        }
    }
}