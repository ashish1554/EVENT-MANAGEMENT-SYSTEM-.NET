using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EVENT_MANAGEMENT_SYSTEM.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IEventService _eventService;
        public AdminController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventDto request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result=await _eventService.CreateEvent(request, userId);
            return Ok(result);
        }

        [HttpPatch("UpdateEvent{id:int}")]
        public async Task<IActionResult> UpdateEventById(int id,EventDto request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result =await _eventService.UpdateEvent(id, request,userId);
            if(result==null)
            {
                return BadRequest("ID Not Found");
            }
            return Ok(result);
        }   


        [HttpDelete("DeleteEvent{id:int}")]
        public async Task<IActionResult> DeleteEventById(int id)
        {
            var result=await _eventService.DeleteEventById(id);
            if(result == null)
            {
                return BadRequest("Id Not Found");
            }
            return Ok(result);
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

        [HttpGet("GetAllRegistrations")]
        public async Task<IActionResult> GetAllRegistration()
        {
            var result = await _eventService.GetAllRegistration();
            if(result.Count==0)
            {
                return NotFound("No Registration Found");
            }
            return Ok(result);
        }
    }
}
