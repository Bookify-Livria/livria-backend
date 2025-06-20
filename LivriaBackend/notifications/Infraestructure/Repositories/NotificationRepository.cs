using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            
        }

        public async Task DeleteAsync(int id)
        {
            var notificationToDelete = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
            if (notificationToDelete != null)
            {
                _context.Notifications.Remove(notificationToDelete);
                
            }
        }
    }
}