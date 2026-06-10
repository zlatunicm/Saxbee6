using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HarvestsController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public HarvestsController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HarvestDto>>> GetAll([FromQuery] int? hiveId)
    {
        var query = _ctx.Harvests.Include(h => h.Hive).AsQueryable();
        if (hiveId.HasValue) query = query.Where(h => h.HiveId == hiveId.Value);
        var list = await query.OrderByDescending(h => h.HarvestedAt)
            .Select(h => new HarvestDto(h.Id, h.Type, h.QuantityKg, h.HarvestedAt, h.Notes, h.HiveId, h.Hive.Name))
            .ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateHarvestDto dto)
    {
        var h = new Harvest { Type = dto.Type, QuantityKg = dto.QuantityKg, HarvestedAt = dto.HarvestedAt, Notes = dto.Notes, HiveId = dto.HiveId };
        _ctx.Harvests.Add(h);
        await _ctx.SaveChangesAsync();
        return Ok(h.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var h = await _ctx.Harvests.FindAsync(id);
        if (h is null) return NotFound();
        _ctx.Harvests.Remove(h);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<ActionResult> Stats()
    {
        var stats = await _ctx.Harvests
            .GroupBy(h => h.Type)
            .Select(g => new { Type = g.Key, TotalKg = g.Sum(h => h.QuantityKg), Count = g.Count() })
            .ToListAsync();
        return Ok(stats);
    }
}
