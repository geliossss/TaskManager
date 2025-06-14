using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;
using TaskManager.Data;
using TaskManager.Domain.Models;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var exists = await _context.Users.AnyAsync(u => u.Login == request.Login);
            if (exists)
                return BadRequest("Пользователь с таким логином уже существует");

            var user = new User
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                SurName = request.SurName,
                Login = request.Login,
                Password = request.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Пользователь зарегистрирован" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.Login == request.Login && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Неверный логин или пароль");

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName
            });
        }
    }
}
