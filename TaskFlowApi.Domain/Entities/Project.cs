namespace TaskFlowApi.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    private readonly List<TaskItem> _tasks = new();

    // EF Core precisa de construtor sem parâmetros
    protected Project() { }

    public Project(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome do projeto não pode ser vazio.", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome do projeto não pode ser vazio.", nameof(name));

        Name = name;
        Description = description;
    }
}