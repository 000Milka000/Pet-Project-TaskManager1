namespace TaskManager_New.DTOs
{
    public class TokenRequestDto
    {
        public int UserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
