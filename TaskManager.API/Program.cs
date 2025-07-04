using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����������� ���� ������

var dbPath = Path.Combine(
    Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..")),
    "TaskManager.Data",
    "TaskManager.Data",
    "taskmanager.db"
);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// ���������� ������������
builder.Services.AddControllers();

// Swagger (��� ������)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger ���������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // ?? �����������

app.Run();
