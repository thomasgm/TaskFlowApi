using TaskFlowApi.Domain.Enums;

namespace TaskFlowApi.Application.DTOs;

public record CreateTaskItemRequest(string Title, string? Description, Guid ProjectId);
public record UpdateTaskItemRequest(string Title, string? Description);
public record UpdateTaskItemStatusRequest(TaskItemStatus Status);

public record TaskItemResponse(
    Guid Id,
    string Title,
    string? Description,
    TaskItemStatus Status,
    Guid ProjectId,
    DateTime CreatedAt
);