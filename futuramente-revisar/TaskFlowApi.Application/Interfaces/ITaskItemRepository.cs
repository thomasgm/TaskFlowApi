using TaskFlowApi.Domain.Entities;

namespace TaskFlowApi.Application.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(Guid projectId);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem taskItem);
    Task UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(TaskItem taskItem);
}