using Microsoft.AspNetCore.Mvc;
using TaskFlowApi.Application.DTOs;
using TaskFlowApi.Application.Interfaces;

namespace TaskFlowApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly ITaskItemService _service;

    public TaskItemsController(ITaskItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _service.GetAllByProjectIdAsync(Guid.Empty);
        return Ok(tasks);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> GetByProject(Guid projectId)
    {
        var tasks = await _service.GetAllByProjectIdAsync(projectId);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _service.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskItemRequest request)
    {
        var task = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskItemRequest request)
    {
        var task = await _service.UpdateAsync(id, request);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskItemStatusRequest request)
    {
        var task = await _service.UpdateStatusAsync(id, request);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }


}