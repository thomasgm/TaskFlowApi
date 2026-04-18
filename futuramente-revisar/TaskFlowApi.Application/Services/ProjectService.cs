using TaskFlowApi.Application.DTOs;
using TaskFlowApi.Application.Interfaces;
using TaskFlowApi.Domain.Entities;

namespace TaskFlowApi.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
    {
        var projects = await _repository.GetAllAsync();
        return projects.Select(ToResponse);
    }

    public async Task<ProjectResponse?> GetByIdAsync(Guid id)
    {
        var project = await _repository.GetByIdAsync(id);
        return project is null ? null : ToResponse(project);
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request)
    {
        var project = new Project(request.Name, request.Description);
        await _repository.AddAsync(project);
        return ToResponse(project);
    }

    public async Task<ProjectResponse?> UpdateAsync(Guid id, UpdateProjectRequest request)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project is null) return null;

        project.Update(request.Name, request.Description);
        await _repository.UpdateAsync(project);
        return ToResponse(project);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project is null) return false;

        await _repository.DeleteAsync(project);
        return true;
    }

    private static ProjectResponse ToResponse(Project p) =>
        new(p.Id, p.Name, p.Description, p.CreatedAt);
}