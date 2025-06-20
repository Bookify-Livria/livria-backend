using System;
using System.ComponentModel.DataAnnotations;
using LivriaBackend.Shared.Resources;
using LivriaBackend.Shared.Validation;
using LivriaBackend.notifications.Application.Validation;

namespace LivriaBackend.notifications.Interfaces.REST.Resources
{
    public record CreateNotificationResource(
        [Required(ErrorMessage = "EmptyField")]
        [ValidNotificationType(ErrorResourceType = typeof(LivriaBackend.notifications.Application.Resources.DataAnnotations), ErrorResourceName = "InvalidNotificationType")]
        string Type,

        [Required(ErrorMessage = "EmptyField")]
        [DataType(DataType.DateTime)]
        [DateRangeToday(MinimumDate = "1900-01-01", ErrorResourceType = typeof(SharedResource), ErrorResourceName = "DateNotInRange")]
        DateTime Date
    );
}