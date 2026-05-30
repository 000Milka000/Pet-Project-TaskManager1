using TaskManager_New.Models;

namespace TaskManager_New.DTOs
{
    public class UserApiModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
