using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Domain.Models;
using Status = TaskManager.Domain.Models.TaskStatus;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TasksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskItem task)
        {
            if (task == null)
                return BadRequest();

            task.CreatedAt = DateTime.UtcNow;
            task.Status = Status.Created;

            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();

            return Ok(task);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredTasks(
            [FromQuery] Status? status,
            [FromQuery] string? author,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var query = _dbContext.Tasks
                .Include(t => t.User)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(t => (t.User.FirstName + " " + t.User.LastName).Contains(author));

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            var result = await query.ToListAsync();
            return Ok(result);
        }


    }

}
