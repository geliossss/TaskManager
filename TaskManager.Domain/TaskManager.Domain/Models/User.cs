namespace TaskManager.Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string SurName { get; set; } = "";
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";

    public List<Comment> Comments { get; set; } = new();
}
