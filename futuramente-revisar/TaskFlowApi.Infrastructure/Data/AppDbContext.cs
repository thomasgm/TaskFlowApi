using Microsoft.EntityFrameworkCore;
using TaskFlowApi.Domain.Entities;
using TaskFlowApi.Infrastructure.Data.Mappings;

namespace TaskFlowApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectMapping());
        modelBuilder.ApplyConfiguration(new TaskItemMapping());
    }
}