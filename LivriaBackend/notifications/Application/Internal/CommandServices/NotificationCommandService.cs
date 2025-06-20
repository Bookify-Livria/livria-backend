using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Commands;
using LivriaBackend.notifications.Domain.Model.Repositories;
using LivriaBackend.notifications.Domain.Model.Services;
using LivriaBackend.Shared.Domain.Repositories; 
using System;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Application.Internal.CommandServices
{
    public class NotificationCommandService : INotificationCommandService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationCommandService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Notification> Handle(CreateNotificationCommand command)
        {
            
            var notification = new Notification(command.Type, command.Date);
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();
            return notification;
        }

        public async Task Delete(DeleteNotificationCommand command)
        {
            var notificationToDelete = await _notificationRepository.GetByIdAsync(command.NotificationId);
            if (notificationToDelete == null)
            {
                throw new ArgumentException($"Notification with ID {command.NotificationId} not found.");
            }
            await _notificationRepository.DeleteAsync(command.NotificationId);
            await _unitOfWork.CompleteAsync();
        }
    }
}