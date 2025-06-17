using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Domain.Models;
using Status = TaskManager.Domain.Models.TaskStatus;
public class TaskService
{
    private readonly AppDbContext dbContext;

    public TaskService(AppDbContext context)
    {
        dbContext = context;
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await dbContext.Tasks
            .Include(t => t.User)
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .ToListAsync();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(TaskItem task, int userId)
    {
        if (task.UserId != userId)
            throw new UnauthorizedAccessException("Изменять задачу может только автор");

        task.Status = (Status)(((int)task.Status + 1) % 3);
        await dbContext.SaveChangesAsync();
    }

    public async Task SetCloseDateAsync(TaskItem task, int userId)
    {
        if (task.UserId != userId)
            throw new UnauthorizedAccessException("Закрыть задачу может только автор");

        task.ClosedAt = DateTime.Now;
        await dbContext.SaveChangesAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
    }
}
