using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Domain.Model.Services
{
    public interface INotificationQueryService
    {
        Task<Notification> Handle(GetNotificationByIdQuery query);
        Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query);
    }
}