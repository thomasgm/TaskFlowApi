using Microsoft.EntityFrameworkCore;
using TaskFlowApi.Application.Interfaces;
using TaskFlowApi.Domain.Entities;
using TaskFlowApi.Infrastructure.Data;

namespace TaskFlowApi.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TaskItem taskItem)
    {
        await _context.TaskItems.AddAsync(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem taskItem)
    {
        _context.TaskItems.Update(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem taskItem)
    {
        _context.TaskItems.Remove(taskItem);
        await _context.SaveChangesAsync();
    }
}