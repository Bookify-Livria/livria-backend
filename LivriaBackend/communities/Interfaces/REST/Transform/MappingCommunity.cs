using AutoMapper;
using LivriaBackend.communities.Domain.Model.Aggregates;
using LivriaBackend.communities.Domain.Model.Commands;
using LivriaBackend.communities.Interfaces.REST.Resources;

namespace LivriaBackend.communities.Interfaces.REST.Transform
{
    /// <summary>
    /// AutoMapper profile for the Communities context.
    /// </summary>
    public class CommunitiesMappingProfile : Profile
    {
        public CommunitiesMappingProfile()
        {
            // Mapping creation resources to commands
            CreateMap<CreateCommunityResource, CreateCommunityCommand>();
            CreateMap<CreatePostResource, CreatePostCommand>();

            // Mapping aggregates to response resources
            CreateMap<Community, CommunityResource>();
            CreateMap<Post, PostResource>();

            // NEW: Mapping for JoinCommunity command and UserCommunity resource
            CreateMap<JoinCommunityResource, JoinCommunityCommand>();
            CreateMap<UserCommunity, UserCommunityResource>();
        }
    }
}