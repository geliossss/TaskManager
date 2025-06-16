using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение базы данных

var dbPath = Path.Combine(
    Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..")),
    "TaskManager.Data",
    "TaskManager.Data",
    "taskmanager.db"
);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Добавление контроллеров
builder.Services.AddControllers();

// Swagger (для тестов)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger интерфейс
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // ?? обязательно

app.Run();
