using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Models;

namespace EVENT_MANAGEMENT_SYSTEM.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventDto>  CreateEvent(EventDto request,int CreaterId);
        Task<EventDto> UpdateEvent(int id, EventDto request,int updaterId);

        Task<bool> DeleteEventById(int id);

        Task<List<EventDto>> GetAllEvents();

        Task<List<RegistrationResponseDto>> GetAllRegistration();

        Task<EventDto> UpdateOrganizerEvent(int id, EventDto request,int organizerId);

        Task<bool> RegisterForEvents(int userId,int eventId);
        Task<bool> EventCancelation(int userId, int eventId);
    }
}
