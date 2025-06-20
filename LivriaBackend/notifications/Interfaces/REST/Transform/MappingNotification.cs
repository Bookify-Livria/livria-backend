using AutoMapper;
using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Commands;
using LivriaBackend.notifications.Interfaces.REST.Resources;
using LivriaBackend.notifications.Domain.Model.ValueObjects; // Nuevo using
using System; 

namespace LivriaBackend.notifications.Interfaces.REST.Transform
{
    public class MappingNotification : Profile
    {
        public MappingNotification()
        {
            CreateMap<CreateNotificationResource, CreateNotificationCommand>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => GetNotificationType(src.Type)));
            
            CreateMap<Notification, NotificationResource>();
        }

        
        private ENotificationType GetNotificationType(string typeString)
        {
            if (Enum.TryParse(typeString, true, out ENotificationType type))
            {
                return type;
            }
            
            return ENotificationType.Default;
        }
    }
}