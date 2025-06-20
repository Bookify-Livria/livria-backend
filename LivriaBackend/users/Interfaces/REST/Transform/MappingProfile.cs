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
            // Mapeos para UserClient
            CreateMap<CreateUserClientResource, CreateUserClientCommand>();
            CreateMap<UserClient, UserClientResource>();
            CreateMap<UpdateUserClientResource, UpdateUserClientCommand>();

            // Mapeos para UserAdmin
            // REMOVIDO: CreateMap<CreateUserAdminResource, CreateUserAdminCommand>();
            CreateMap<UserAdmin, UserAdminResource>();
            CreateMap<UpdateUserAdminResource, UpdateUserAdminCommand>();

            // Mapeos para User (generales)
            CreateMap<User, UserResource>();

            // REMOVIDO: Mapeos para DeleteUserAdminCommand si existían
        }
    }
}