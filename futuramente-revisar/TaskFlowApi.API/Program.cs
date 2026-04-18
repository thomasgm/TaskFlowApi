using Microsoft.EntityFrameworkCore;
using TaskFlowApi.Application.Interfaces;
using TaskFlowApi.Application.Services;
using TaskFlowApi.Infrastructure.Data;
using TaskFlowApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=taskflow.db"));

// Serviços
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

// Repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

// Aplica migrations automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // db.Database.EnsureCreated();
}

app.Run();