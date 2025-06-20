using AutoMapper;
using LivriaBackend.notifications.Domain.Model.Aggregates;
using LivriaBackend.notifications.Domain.Model.Commands;
using LivriaBackend.notifications.Domain.Model.Queries;
using LivriaBackend.notifications.Domain.Model.Services;
using LivriaBackend.notifications.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LivriaBackend.notifications.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    [Produces(MediaTypeNames.Application.Json)]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationCommandService _notificationCommandService;
        private readonly INotificationQueryService _notificationQueryService;
        private readonly IMapper _mapper;

        public NotificationController(
            INotificationCommandService notificationCommandService,
            INotificationQueryService notificationQueryService,
            IMapper mapper)
        {
            _notificationCommandService = notificationCommandService;
            _notificationQueryService = notificationQueryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<NotificationResource>> CreateNotification([FromBody] CreateNotificationResource resource)
        {
            var createCommand = _mapper.Map<CreateNotificationCommand>(resource);
            
            if (createCommand.Date == default(DateTime)) {
                createCommand = createCommand with { Date = DateTime.UtcNow };
            }

            Notification notification = await _notificationCommandService.Handle(createCommand);
            var notificationResource = _mapper.Map<NotificationResource>(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notificationResource);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationResource>>> GetAllNotifications()
        {
            var query = new GetAllNotificationsQuery();
            var notifications = await _notificationQueryService.Handle(query);
            var resources = _mapper.Map<IEnumerable<NotificationResource>>(notifications);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationResource>> GetNotificationById(int id)
        {
            var query = new GetNotificationByIdQuery(id);
            var notification = await _notificationQueryService.Handle(query);

            if (notification == null)
            {
                return NotFound();
            }

            var resource = _mapper.Map<NotificationResource>(notification);
            return Ok(resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var deleteCommand = new DeleteNotificationCommand(id);
            try
            {
                await _notificationCommandService.Delete(deleteCommand);
                return NoContent(); // 204 No Content para eliminación exitosa
            }
            catch (ArgumentException ex) // Captura si la notificación no se encuentra
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}