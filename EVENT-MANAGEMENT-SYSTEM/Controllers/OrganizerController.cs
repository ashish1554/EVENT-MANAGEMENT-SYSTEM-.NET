using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EVENT_MANAGEMENT_SYSTEM.Controllers
{
    [Authorize(Roles="Organizer")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrganizerController : ControllerBase
    {
        private readonly IEventService _eventService;
        public OrganizerController(IEventService eventService)
        {
            _eventService= eventService;
        }
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventDto request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _eventService.CreateEvent(request, userId);
            return Ok(result);
        }

        [HttpPost("UpdateEventById/{id:int}")]
        public async Task<IActionResult> UpdateEvent(EventDto eventDto,int id)
        {
            var organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var organizerEvent= await _eventService.UpdateOrganizerEvent(id,eventDto, organizerId);
            if(organizerEvent == null)
            {
                return BadRequest("Event not found for this organizer to update");
            }
            return Ok(organizerEvent);
        }

    }
}
