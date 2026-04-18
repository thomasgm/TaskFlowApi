using TaskFlowApi.Application.DTOs;
using TaskFlowApi.Application.Interfaces;
using TaskFlowApi.Domain.Entities;

namespace TaskFlowApi.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _repository;

    public TaskItemService(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItemResponse>> GetAllByProjectIdAsync(Guid projectId)
    {
        var tasks = await _repository.GetAllByProjectIdAsync(projectId);
        return tasks.Select(ToResponse);
    }

    public async Task<TaskItemResponse?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);
        return task is null ? null : ToResponse(task);
    }

    public async Task<TaskItemResponse> CreateAsync(CreateTaskItemRequest request)
    {
        var task = new TaskItem(request.Title, request.ProjectId, request.Description);
        await _repository.AddAsync(task);
        return ToResponse(task);
    }

    public async Task<TaskItemResponse?> UpdateAsync(Guid id, UpdateTaskItemRequest request)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null) return null;

        task.Update(request.Title, request.Description);
        await _repository.UpdateAsync(task);
        return ToResponse(task);
    }

    public async Task<TaskItemResponse?> UpdateStatusAsync(Guid id, UpdateTaskItemStatusRequest request)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null) return null;

        task.UpdateStatus(request.Status);
        await _repository.UpdateAsync(task);
        return ToResponse(task);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null) return false;

        await _repository.DeleteAsync(task);
        return true;
    }

    private static TaskItemResponse ToResponse(TaskItem t) =>
        new(t.Id, t.Title, t.Description, t.Status, t.ProjectId, t.CreatedAt);
}