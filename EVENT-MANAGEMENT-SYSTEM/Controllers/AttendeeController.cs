using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace EVENT_MANAGEMENT_SYSTEM.Controllers
{
    [EnableRateLimiting("fixed")]
    [Authorize(Roles="Attendee")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly IEventService _eventService;
        public AttendeeController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpPost("RegisterForEvents")]
        public async Task<IActionResult> RegisterForEvents([FromQuery] int eventId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _eventService.RegisterForEvents(userId,eventId);
            if(!result)
            {
                return NotFound("Event With this Id is not found");
            }
            return Ok("Event Registration is successfull completed...");
        }
        [HttpDelete("cancelEvent")]
        public async Task<IActionResult> EventCancelation([FromQuery] int eventId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _eventService.EventCancelation(userId, eventId);
            if (!result)
            {
                return NotFound("Event With this Id is not found");
            }
            return Ok("Event Registration is canceled successfully...");
        }

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();
            if (result.Count == 0)
            {
                return NotFound("No Event Found");
            }
            return Ok(result);

        }
    }
}
