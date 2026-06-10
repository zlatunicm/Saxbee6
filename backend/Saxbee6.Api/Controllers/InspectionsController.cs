using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InspectionsController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public InspectionsController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InspectionDto>>> GetAll([FromQuery] int? hiveId)
    {
        var query = _ctx.Inspections.Include(i => i.Hive).AsQueryable();
        if (hiveId.HasValue) query = query.Where(i => i.HiveId == hiveId.Value);

        var list = await query.OrderByDescending(i => i.InspectedAt)
            .Select(i => new InspectionDto(i.Id, i.InspectedAt, i.Strength, i.BroodPattern, i.Temperament,
                i.QueenSeen, i.EggsSeen, i.QueenCellsPresent, i.SwarmingSign,
                i.HoneyStoresFrames, i.BroodFrames, i.BeesFrames, i.VarroaCount,
                i.Notes, i.Weather, i.TemperatureCelsius, i.HiveId, i.Hive.Name))
            .ToListAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InspectionDto>> Get(int id)
    {
        var i = await _ctx.Inspections.Include(i => i.Hive).FirstOrDefaultAsync(i => i.Id == id);
        if (i is null) return NotFound();
        return Ok(new InspectionDto(i.Id, i.InspectedAt, i.Strength, i.BroodPattern, i.Temperament,
            i.QueenSeen, i.EggsSeen, i.QueenCellsPresent, i.SwarmingSign,
            i.HoneyStoresFrames, i.BroodFrames, i.BeesFrames, i.VarroaCount,
            i.Notes, i.Weather, i.TemperatureCelsius, i.HiveId, i.Hive.Name));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateInspectionDto dto)
    {
        var inspection = new Inspection
        {
            InspectedAt = dto.InspectedAt, Strength = dto.Strength, BroodPattern = dto.BroodPattern,
            Temperament = dto.Temperament, QueenSeen = dto.QueenSeen, EggsSeen = dto.EggsSeen,
            QueenCellsPresent = dto.QueenCellsPresent, SwarmingSign = dto.SwarmingSign,
            HoneyStoresFrames = dto.HoneyStoresFrames, BroodFrames = dto.BroodFrames, BeesFrames = dto.BeesFrames,
            VarroaCount = dto.VarroaCount, Notes = dto.Notes, Weather = dto.Weather,
            TemperatureCelsius = dto.TemperatureCelsius, HiveId = dto.HiveId
        };
        _ctx.Inspections.Add(inspection);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = inspection.Id }, inspection.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var inspection = await _ctx.Inspections.FindAsync(id);
        if (inspection is null) return NotFound();
        _ctx.Inspections.Remove(inspection);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
