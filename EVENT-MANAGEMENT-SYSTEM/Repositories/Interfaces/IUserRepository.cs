using EVENT_MANAGEMENT_SYSTEM.Models;

namespace EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        //Task<bool> CheckEmail(string email);
        Task AddUser(User user);

         
       
    }
}
