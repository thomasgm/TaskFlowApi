using TaskFlowApi.Application.DTOs;

namespace TaskFlowApi.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync();
    Task<ProjectResponse?> GetByIdAsync(Guid id);
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request);
    Task<ProjectResponse?> UpdateAsync(Guid id, UpdateProjectRequest request);
    Task<bool> DeleteAsync(Guid id);
}