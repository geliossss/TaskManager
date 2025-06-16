﻿namespace TaskManager.Domain.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int? UserId { get; set; }
    public User? User { get; set; }

    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; } = null!;
}
