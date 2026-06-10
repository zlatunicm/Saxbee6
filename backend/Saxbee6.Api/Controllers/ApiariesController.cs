using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data;
using Saxbee6.Api.Data.Models;
using Saxbee6.Api.DTOs;

namespace Saxbee6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiariesController : ControllerBase
{
    private readonly SaxBeeContext _ctx;
    public ApiariesController(SaxBeeContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApiaryDto>>> GetAll()
    {
        var apiaries = await _ctx.Apiaries
            .Include(a => a.Hives)
            .Select(a => new ApiaryDto(a.Id, a.Name, a.Description, a.Location, a.Latitude, a.Longitude, a.CreatedAt, a.Hives.Count))
            .ToListAsync();
        return Ok(apiaries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiaryDto>> Get(int id)
    {
        var a = await _ctx.Apiaries.Include(a => a.Hives).FirstOrDefaultAsync(a => a.Id == id);
        if (a is null) return NotFound();
        return Ok(new ApiaryDto(a.Id, a.Name, a.Description, a.Location, a.Latitude, a.Longitude, a.CreatedAt, a.Hives.Count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiaryDto>> Create(CreateApiaryDto dto)
    {
        var apiary = new Apiary { Name = dto.Name, Description = dto.Description, Location = dto.Location, Latitude = dto.Latitude, Longitude = dto.Longitude };
        _ctx.Apiaries.Add(apiary);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = apiary.Id }, new ApiaryDto(apiary.Id, apiary.Name, apiary.Description, apiary.Location, apiary.Latitude, apiary.Longitude, apiary.CreatedAt, 0));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateApiaryDto dto)
    {
        var apiary = await _ctx.Apiaries.FindAsync(id);
        if (apiary is null) return NotFound();
        apiary.Name = dto.Name;
        apiary.Description = dto.Description;
        apiary.Location = dto.Location;
        apiary.Latitude = dto.Latitude;
        apiary.Longitude = dto.Longitude;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var apiary = await _ctx.Apiaries.FindAsync(id);
        if (apiary is null) return NotFound();
        _ctx.Apiaries.Remove(apiary);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
