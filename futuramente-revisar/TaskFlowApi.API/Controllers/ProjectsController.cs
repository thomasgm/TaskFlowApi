using Microsoft.AspNetCore.Mvc;
using TaskFlowApi.Application.DTOs;
using TaskFlowApi.Application.Interfaces;

namespace TaskFlowApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _service.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _service.GetByIdAsync(id);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest request)
    {
        var project = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectRequest request)
    {
        var project = await _service.UpdateAsync(id, request);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}