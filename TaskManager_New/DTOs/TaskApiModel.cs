using TaskManager_New.Models;

namespace TaskManager_New.DTOs
{
    public class TaskApiModel   
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
