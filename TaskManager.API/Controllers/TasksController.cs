using Microsoft.AspNetCore.Mvc;
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

    }

}
