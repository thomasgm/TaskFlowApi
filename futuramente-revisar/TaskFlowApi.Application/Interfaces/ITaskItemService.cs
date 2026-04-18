using TaskFlowApi.Application.DTOs;
using TaskFlowApi.Domain.Enums;

namespace TaskFlowApi.Application.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponse>> GetAllByProjectIdAsync(Guid projectId);
    Task<TaskItemResponse?> GetByIdAsync(Guid id);
    Task<TaskItemResponse> CreateAsync(CreateTaskItemRequest request);
    Task<TaskItemResponse?> UpdateAsync(Guid id, UpdateTaskItemRequest request);
    Task<TaskItemResponse?> UpdateStatusAsync(Guid id, UpdateTaskItemStatusRequest request);
    Task<bool> DeleteAsync(Guid id);
}