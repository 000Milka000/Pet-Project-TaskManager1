using TaskManager_New.Data;
using TaskManager_New.Models;



namespace TaskManager_New.Services.Users
{
    public class UserServices : IUserServices
    {
        public readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
