using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueensController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public QueensController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet("{id}")]
    public async Task<ActionResult<QueenDto>> Get(int id)
    {
        var q = await _ctx.Queens.FindAsync(id);
        if (q is null) return NotFound();
        return Ok(new QueenDto(q.Id, q.Name, q.YearBorn, q.Status, q.Origin, q.MarkingColor, q.IsMarked, q.IsClipped, q.Notes, q.HiveId));
    }

    [HttpPost]
    public async Task<ActionResult<QueenDto>> Create(CreateQueenDto dto)
    {
        var existing = await _ctx.Queens.FirstOrDefaultAsync(q => q.HiveId == dto.HiveId);
        if (existing is not null) _ctx.Queens.Remove(existing);

        var queen = new Queen { Name = dto.Name, YearBorn = dto.YearBorn, Status = dto.Status, Origin = dto.Origin, MarkingColor = dto.MarkingColor, IsMarked = dto.IsMarked, IsClipped = dto.IsClipped, Notes = dto.Notes, HiveId = dto.HiveId };
        _ctx.Queens.Add(queen);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = queen.Id }, new QueenDto(queen.Id, queen.Name, queen.YearBorn, queen.Status, queen.Origin, queen.MarkingColor, queen.IsMarked, queen.IsClipped, queen.Notes, queen.HiveId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateQueenDto dto)
    {
        var queen = await _ctx.Queens.FindAsync(id);
        if (queen is null) return NotFound();
        queen.Name = dto.Name; queen.YearBorn = dto.YearBorn; queen.Status = dto.Status;
        queen.Origin = dto.Origin; queen.MarkingColor = dto.MarkingColor;
        queen.IsMarked = dto.IsMarked; queen.IsClipped = dto.IsClipped; queen.Notes = dto.Notes;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var queen = await _ctx.Queens.FindAsync(id);
        if (queen is null) return NotFound();
        _ctx.Queens.Remove(queen);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
