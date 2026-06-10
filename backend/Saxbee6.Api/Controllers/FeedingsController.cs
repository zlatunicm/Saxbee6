using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingsController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public FeedingsController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedingDto>>> GetAll([FromQuery] int? hiveId)
    {
        var query = _ctx.Feedings.Include(f => f.Hive).AsQueryable();
        if (hiveId.HasValue) query = query.Where(f => f.HiveId == hiveId.Value);
        var list = await query.OrderByDescending(f => f.FedAt)
            .Select(f => new FeedingDto(f.Id, f.FeedType, f.QuantityKg, f.FedAt, f.Notes, f.HiveId, f.Hive.Name))
            .ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateFeedingDto dto)
    {
        var f = new Feeding { FeedType = dto.FeedType, QuantityKg = dto.QuantityKg, FedAt = dto.FedAt, Notes = dto.Notes, HiveId = dto.HiveId };
        _ctx.Feedings.Add(f);
        await _ctx.SaveChangesAsync();
        return Ok(f.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var f = await _ctx.Feedings.FindAsync(id);
        if (f is null) return NotFound();
        _ctx.Feedings.Remove(f);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
