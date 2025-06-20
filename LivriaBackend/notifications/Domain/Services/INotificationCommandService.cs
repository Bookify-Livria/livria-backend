using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Commands;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Domain.Model.Services
{
    public interface INotificationCommandService
    {
        Task<Notification> Handle(CreateNotificationCommand command);
        Task Delete(DeleteNotificationCommand command);
    }
}