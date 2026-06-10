using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreatmentsController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public TreatmentsController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreatmentDto>>> GetAll([FromQuery] int? hiveId)
    {
        var query = _ctx.Treatments.Include(t => t.Hive).AsQueryable();
        if (hiveId.HasValue) query = query.Where(t => t.HiveId == hiveId.Value);
        var list = await query.OrderByDescending(t => t.StartDate)
            .Select(t => new TreatmentDto(t.Id, t.Type, t.Product, t.Dosage, t.StartDate, t.EndDate, t.Notes, t.HiveId, t.Hive.Name))
            .ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateTreatmentDto dto)
    {
        var t = new Treatment { Type = dto.Type, Product = dto.Product, Dosage = dto.Dosage, StartDate = dto.StartDate, EndDate = dto.EndDate, Notes = dto.Notes, HiveId = dto.HiveId };
        _ctx.Treatments.Add(t);
        await _ctx.SaveChangesAsync();
        return Ok(t.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTreatmentDto dto)
    {
        var t = await _ctx.Treatments.FindAsync(id);
        if (t is null) return NotFound();
        t.Type = dto.Type; t.Product = dto.Product; t.Dosage = dto.Dosage;
        t.StartDate = dto.StartDate; t.EndDate = dto.EndDate; t.Notes = dto.Notes;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var t = await _ctx.Treatments.FindAsync(id);
        if (t is null) return NotFound();
        _ctx.Treatments.Remove(t);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
