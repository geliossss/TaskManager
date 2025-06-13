using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ClosedAt { get; set; }
        public int? CommentaryId { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        public User User { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
    }

    public enum TaskStatus
    {
        Created,
        InProgress,
        Completed
    }
}