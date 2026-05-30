using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TaskManager_New.Models
{
    [Index(nameof(Login), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public List<TaskItem> Tasks { get; set; }
    }
}
