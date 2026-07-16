namespace TaskManager_New.DTOs
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
