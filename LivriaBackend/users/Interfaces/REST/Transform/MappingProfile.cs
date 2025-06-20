using AutoMapper;
using LivriaBackend.users.Domain.Model.Aggregates;
using LivriaBackend.users.Domain.Model.Commands;
using LivriaBackend.users.Interfaces.REST.Resources;

namespace LivriaBackend.users.Interfaces.REST.Transform
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<CreateUserClientResource, CreateUserClientCommand>();
            CreateMap<UserClient, UserClientResource>();
            CreateMap<UpdateUserClientResource, UpdateUserClientCommand>();

            CreateMap<UserAdmin, UserAdminResource>();
            CreateMap<UpdateUserAdminResource, UpdateUserAdminCommand>();

            CreateMap<User, UserResource>();

        }
    }
}