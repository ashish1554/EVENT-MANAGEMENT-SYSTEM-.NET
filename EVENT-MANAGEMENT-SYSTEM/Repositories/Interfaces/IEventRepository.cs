using EVENT_MANAGEMENT_SYSTEM.Models;
using System.Collections;

namespace EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task CreateEvent(Event creteEvent);
        Task<Event?> FindById(int id);

        Task SaveChanges();

        Task DeleteEvent(Event deleteEvent);

        Task<List<Event>> GetAllEvents();

        Task<List<Registration>> GetAllRegistrations();

        Task<Event> FindByOrganizerId(int id, int OrganizerId);

        Task RegisterForEvents(Registration registration);

        Task<Registration> CancelRegistration(int userId, int eventId);
    }
}
