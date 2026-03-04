using AutoMapper;
using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Models;
using EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;

namespace EVENT_MANAGEMENT_SYSTEM.Services.Implementations
{
    public class EventServices : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventServices(IMapper mapper,IEventRepository eventRepository)
        {
            _mapper= mapper;
            _eventRepository= eventRepository;
        }
        public async Task<EventDto> CreateEvent(EventDto request,int CreaterId)
        {

            var newEvent=_mapper.Map<Event>(request);
            newEvent.CreatedBy = CreaterId;
            await _eventRepository.CreateEvent(newEvent);
            var eventResponse = _mapper.Map<EventDto>(newEvent);
            return eventResponse;

        }

        public async Task<EventDto> UpdateEvent(int id,EventDto request,int updaterId)
        {
            var existingEvent=await _eventRepository.FindById(id);
            if (existingEvent==null)
            {
                return null;
            }

             existingEvent.UpdatedBy = updaterId;
            _mapper.Map(request, existingEvent);
            await _eventRepository.SaveChanges();
            var eventResponse = _mapper.Map<EventDto>(existingEvent);
            return eventResponse;
        }

        public async Task<bool> DeleteEventById(int id)
        {
            var existingEvent = await _eventRepository.FindById(id);
            if (existingEvent == null)
            {
                return false;
            }

            await _eventRepository.DeleteEvent(existingEvent);
            return true;
        }

        public async Task<List<EventDto>> GetAllEvents()
        {
            var events=await _eventRepository.GetAllEvents();
            var eventResponse = _mapper.Map<List<EventDto>>(events);
            return eventResponse;
        }
        public async Task<List<RegistrationResponseDto>> GetAllRegistration()
        {
            var registrations = await _eventRepository.GetAllRegistrations();
            var details = registrations.Select(reg => new
            RegistrationResponseDto()
            {
                Name=reg.User.Name,
                EventName=reg.Event.EventName
            }
            ).ToList();
            return  details;
        }

       public async Task<EventDto> UpdateOrganizerEvent(int id, EventDto request, int organizerId)
        {
            var organizerEvent = await _eventRepository.FindByOrganizerId(id, organizerId);

            if (organizerEvent == null)
            {
                return null;
            }

            organizerEvent.UpdatedBy = organizerId;
            _mapper.Map(request,organizerEvent);
            await _eventRepository.SaveChanges();
            var eventResponse = _mapper.Map<EventDto>(organizerEvent);
            return eventResponse;
        }

        public async Task<bool> RegisterForEvents(int userId,int eventId)
        {
            var checkEvent = await _eventRepository.FindById(eventId);
            if (checkEvent == null)
            {
                return false;
            }
            var register = new Registration();
            register.UserId = userId;
            register.EventId= eventId;
            await _eventRepository.RegisterForEvents(register);
            return true;
        }
        public async Task<bool> EventCancelation(int userId, int eventId)
        {
            var checkEvent = await _eventRepository.FindById(eventId);
            if (checkEvent == null)
            {
                return false;
            }
            var findEventToCancel = await _eventRepository.CancelRegistration(userId, eventId);
            if(findEventToCancel == null)
            {
                return false;
            }
            findEventToCancel.Status = "Cancel";
            await _eventRepository.SaveChanges();
            return true;
        }
    }
}
