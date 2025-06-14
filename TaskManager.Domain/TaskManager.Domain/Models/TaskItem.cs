namespace TaskManager.Domain.Models;

public class TaskItem
{
    public int TaskItemId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public TaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ClosedAt { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public List<Comment> Comments { get; set; } = new();
}
