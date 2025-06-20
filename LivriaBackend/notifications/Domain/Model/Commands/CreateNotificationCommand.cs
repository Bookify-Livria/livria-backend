using System;
using LivriaBackend.notifications.Domain.Model.ValueObjects;

namespace LivriaBackend.notifications.Domain.Model.Commands
{
    public record CreateNotificationCommand(
        ENotificationType Type, 
        DateTime Date 
    );
}