using System;

namespace LivriaBackend.notifications.Interfaces.REST.Resources
{
    public record CreateNotificationResource(
        string Type, 
        DateTime Date
    );
}