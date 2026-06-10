using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public TasksController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeeTaskDto>>> GetAll([FromQuery] bool? completed)
    {
        var query = _ctx.Tasks.AsQueryable();
        if (completed.HasValue) query = query.Where(t => t.IsCompleted == completed.Value);
        var list = await query.OrderBy(t => t.DueDate).ThenBy(t => t.Priority)
            .Select(t => new BeeTaskDto(t.Id, t.Title, t.Description, t.Priority, t.DueDate, t.IsCompleted, t.CreatedAt, t.HiveId, t.ApiaryId))
            .ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateBeeTaskDto dto)
    {
        var task = new BeeTask { Title = dto.Title, Description = dto.Description, Priority = dto.Priority, DueDate = dto.DueDate, HiveId = dto.HiveId, ApiaryId = dto.ApiaryId };
        _ctx.Tasks.Add(task);
        await _ctx.SaveChangesAsync();
        return Ok(task.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBeeTaskDto dto)
    {
        var task = await _ctx.Tasks.FindAsync(id);
        if (task is null) return NotFound();
        task.Title = dto.Title; task.Description = dto.Description; task.Priority = dto.Priority;
        task.DueDate = dto.DueDate; task.IsCompleted = dto.IsCompleted;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> ToggleComplete(int id)
    {
        var task = await _ctx.Tasks.FindAsync(id);
        if (task is null) return NotFound();
        task.IsCompleted = !task.IsCompleted;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _ctx.Tasks.FindAsync(id);
        if (task is null) return NotFound();
        _ctx.Tasks.Remove(task);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
