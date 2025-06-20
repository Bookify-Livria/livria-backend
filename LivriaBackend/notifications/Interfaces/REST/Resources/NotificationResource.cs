using System;
using LivriaBackend.notifications.Domain.Model.ValueObjects; // Nuevo using

namespace LivriaBackend.notifications.Interfaces.REST.Resources
{
    public record NotificationResource(
        int Id,
        DateTime Date,
        ENotificationType Type, 
        string Title,
        string Content
    );
}