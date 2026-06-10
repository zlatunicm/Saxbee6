using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HivesController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public HivesController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HiveSummaryDto>>> GetAll([FromQuery] int? apiaryId)
    {
        var query = _ctx.Hives.Include(h => h.Queen).Include(h => h.Inspections).AsQueryable();
        if (apiaryId.HasValue) query = query.Where(h => h.ApiaryId == apiaryId.Value);

        var hives = await query.Select(h => new HiveSummaryDto(
            h.Id, h.Name, h.Type, h.Status, h.Color, h.ApiaryId,
            h.Queen != null ? h.Queen.Status : null,
            h.Inspections.OrderByDescending(i => i.InspectedAt).Select(i => (DateTime?)i.InspectedAt).FirstOrDefault()
        )).ToListAsync();

        return Ok(hives);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HiveDto>> Get(int id)
    {
        var h = await _ctx.Hives.Include(h => h.Queen).Include(h => h.Apiary).FirstOrDefaultAsync(h => h.Id == id);
        if (h is null) return NotFound();
        var queenDto = h.Queen is not null ? new QueenDto(h.Queen.Id, h.Queen.Name, h.Queen.YearBorn, h.Queen.Status, h.Queen.Origin, h.Queen.MarkingColor, h.Queen.IsMarked, h.Queen.IsClipped, h.Queen.Notes, h.Queen.HiveId) : null;
        return Ok(new HiveDto(h.Id, h.Name, h.Description, h.Type, h.Status, h.EstablishedDate, h.Color, h.Notes, h.ApiaryId, h.Apiary.Name, queenDto));
    }

    [HttpPost]
    public async Task<ActionResult<HiveDto>> Create(CreateHiveDto dto)
    {
        var hive = new Hive
        {
            Name = dto.Name, Description = dto.Description, Type = dto.Type,
            Color = dto.Color, Notes = dto.Notes, ApiaryId = dto.ApiaryId,
            EstablishedDate = dto.EstablishedDate ?? DateTime.UtcNow
        };
        _ctx.Hives.Add(hive);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = hive.Id }, hive.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateHiveDto dto)
    {
        var hive = await _ctx.Hives.FindAsync(id);
        if (hive is null) return NotFound();
        hive.Name = dto.Name; hive.Description = dto.Description; hive.Type = dto.Type;
        hive.Status = dto.Status; hive.Color = dto.Color; hive.Notes = dto.Notes;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var hive = await _ctx.Hives.FindAsync(id);
        if (hive is null) return NotFound();
        _ctx.Hives.Remove(hive);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
