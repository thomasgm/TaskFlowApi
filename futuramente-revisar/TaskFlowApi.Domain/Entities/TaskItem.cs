using TaskFlowApi.Domain.Enums;

namespace TaskFlowApi.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskItemStatus  Status { get; private set; }
    public Guid ProjectId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected TaskItem() { }

    public TaskItem(string title, Guid projectId, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título da tarefa não pode ser vazio.", nameof(title));

        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId inválido.", nameof(projectId));

        Id = Guid.NewGuid();
        Title = title;
        ProjectId = projectId;
        Description = description;
        Status = TaskItemStatus.Todo;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TaskItemStatus status) => Status = status;

    public void Update(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título não pode ser vazio.", nameof(title));

        Title = title;
        Description = description;
    }
}