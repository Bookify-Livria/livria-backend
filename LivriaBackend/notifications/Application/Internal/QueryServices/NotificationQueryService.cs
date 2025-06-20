using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Queries;
using LivriaBackend.notifications.Domain.Model.Repositories;
using LivriaBackend.notifications.Domain.Model.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Application.Internal.QueryServices
{
    public class NotificationQueryService : INotificationQueryService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueryService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> Handle(GetNotificationByIdQuery query)
        {
            return await _notificationRepository.GetByIdAsync(query.NotificationId);
        }

        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query)
        {
            return await _notificationRepository.GetAllAsync();
        }
    }
}