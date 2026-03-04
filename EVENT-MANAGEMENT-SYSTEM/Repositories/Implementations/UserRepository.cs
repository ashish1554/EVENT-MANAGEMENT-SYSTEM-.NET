using EVENT_MANAGEMENT_SYSTEM.Data;
using EVENT_MANAGEMENT_SYSTEM.Models;
using EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVENT_MANAGEMENT_SYSTEM.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context= context;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Include(r=>r.Role).FirstOrDefaultAsync(e=>e.Email== email);
        }

        //public async Task<bool> CheckEmail(string email)
        //{
        //    return _context.Users.Any(e=>e.Email == email);
        //}


        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        
    }
}
