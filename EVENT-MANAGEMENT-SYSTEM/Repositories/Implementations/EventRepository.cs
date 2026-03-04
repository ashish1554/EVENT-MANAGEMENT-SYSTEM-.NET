using EVENT_MANAGEMENT_SYSTEM.Data;
using EVENT_MANAGEMENT_SYSTEM.Models;
using EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace EVENT_MANAGEMENT_SYSTEM.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context= context;
        }
        public async  Task CreateEvent(Event creteEvent)
        {
            await _context.Events.AddAsync(creteEvent);
            await _context.SaveChangesAsync();
        }
        public async Task<Event?> FindById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(Event deleteEvent)
        {
            _context.Events.Remove(deleteEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEvents()            
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<List<Registration>> GetAllRegistrations()
        {
            return await _context.Registrations.Include(e => e.Event).Include(e => e.User).ToListAsync();
        }

        public async Task<Event> FindByOrganizerId(int id, int OrganizerId)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.EventId == id && e.CreatedBy==OrganizerId);
        }
        public async Task RegisterForEvents(Registration registration)
        {
             await _context.Registrations.AddAsync(registration);
             await _context.SaveChangesAsync();
        }
        public async Task<Registration> CancelRegistration(int userId, int eventId)
        {
            return await _context.Registrations.FirstOrDefaultAsync(e=>e.UserId== userId && e.EventId == eventId);
        }
    }
}
