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
            // --- Mapeos de Resources a Commands ---

            // Para la creación de un UserClient
            CreateMap<CreateUserClientResource, CreateUserClientCommand>();

            // Para la actualización de un UserClient
            CreateMap<UpdateUserClientResource, UpdateUserClientCommand>()
                // CORRECCIÓN: Usar ForMember().Ignore() para ignorar propiedades del DESTINO.
                // Si UserClientId viene como parte de la URL/ruta y no del cuerpo del request,
                // entonces el comando debería recibirlo de otra forma.
                // Esta línea asume que NO quieres mapear UserClientId desde el recurso UpdateUserClientResource.
                .ForMember(dest => dest.UserClientId, opt => opt.Ignore()); 

            // Para la actualización de un UserAdmin
            CreateMap<UpdateUserAdminResource, UpdateUserAdminCommand>()
                // CORRECCIÓN: Usar ForMember().Ignore()
                .ForMember(dest => dest.UserAdminId, opt => opt.Ignore()); 

            // Si tuvieras un comando para actualizar la base User (si es que existe y se usa)
            // CreateMap<UpdateUserResource, UpdateUserCommand>()
            //    .ForMember(dest => dest.UserId, opt => opt.Ignore());

            // --- Mapeos de Aggregates/Domain a Resources (para respuestas de la API) ---
            CreateMap<UserClient, UserClientResource>();
            CreateMap<UserAdmin, UserAdminResource>();
            CreateMap<User, UserResource>();
        }
    }
}