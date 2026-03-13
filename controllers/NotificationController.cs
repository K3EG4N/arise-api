using arise_api.dtos.Responses;
using arise_api.services;
using Microsoft.AspNetCore.Mvc;

namespace arise_api.controllers
{
    [Route("api/[controller]")]
    public class NotificationController(INotificationService service) : Controller
    {
        private readonly INotificationService _service = service;

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<NotificationResponse>>> GetNotifications(Guid userId)
        {
            var users = await _service.GetNotifications(userId);
            return Ok(users);
        }
    }
}
