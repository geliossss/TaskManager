namespace TaskManager.API.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
