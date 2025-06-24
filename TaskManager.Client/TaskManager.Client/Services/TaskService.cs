using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Domain.Models;
using Status = TaskManager.Domain.Models.TaskStatus;

namespace TaskManager.Client.Services
{
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

        public async Task<List<TaskItem>> GetFilteredTasksAsync(string status, string author, DateTime? startDate, DateTime? endDate)
        {
            var query = dbContext.Tasks.AsQueryable();

            // Фильтрация по статусу
            if (!string.IsNullOrEmpty(status) && status != "all")
            {
                if (Enum.TryParse(status, out Status taskStatus))
                {
                    query = query.Where(t => t.Status == taskStatus);
                }
            }

            // Фильтрация по автору
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(t => t.User.LastName.Contains(author) || t.User.FirstName.Contains(author));
            }

            // Фильтрация по дате начала
            if (startDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt >= startDate.Value);
            }

            // Фильтрация по дате окончания
            if (endDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt <= endDate.Value);
            }

            return await query
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
            if (task.Status != Status.Completed)
                task.ClosedAt = null;
            else
                task.ClosedAt = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(int taskId, Status newStatus)
        {
            var task = await dbContext.Tasks.FindAsync(taskId);
            if (task == null)
                throw new Exception("Задача не найдена.");

            task.Status = newStatus;
            if (newStatus == Status.Completed)
                task.ClosedAt = DateTime.Now;
            else
                task.ClosedAt = null;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
        }
    }
}
