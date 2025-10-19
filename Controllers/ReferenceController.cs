using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olympics.Api.Data;

namespace Olympics.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ReferenceController(AppDbContext db) : ControllerBase
{
    [HttpGet("sports")]
    public async Task<ActionResult<IEnumerable<object>>> Sports([FromQuery] string? q)
    {
        var query = db.Sports.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(s => s.SportName.Contains(q));
        var items = await query.OrderBy(s => s.SportName)
            .Select(s => new { s.SportId, s.SportName })
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("events")]
    public async Task<ActionResult<IEnumerable<object>>> Events([FromQuery] int? sportId, [FromQuery] string? q)
    {
        var query = db.Events.AsNoTracking().Include(e => e.Sport).AsQueryable();
        if (sportId is not null) query = query.Where(e => e.SportId == sportId);
        if (!string.IsNullOrWhiteSpace(q)) query = query.Where(e => e.EventName.Contains(q));
        var items = await query.OrderBy(e => e.Sport.SportName).ThenBy(e => e.EventName)
            .Select(e => new { e.EventId, e.EventName, e.SportId, Sport = e.Sport.SportName })
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("games")]
    public async Task<ActionResult<IEnumerable<object>>> Games([FromQuery] string? season, [FromQuery] int? year)
    {
        var query = db.Games.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(season)) query = query.Where(g => g.Season == season);
        if (year is not null) query = query.Where(g => g.Year == year);
        var items = await query.OrderBy(g => g.Year).ThenBy(g => g.Season).ThenBy(g => g.City)
            .Select(g => new { g.GameId, g.Year, g.Season, g.City, g.GamesLabel })
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("nocs")]
    public async Task<ActionResult<IEnumerable<object>>> Nocs([FromQuery] string? q)
    {
        var query = db.Nocs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(n => n.NocId.Contains(q) || (n.NocName != null && n.NocName.Contains(q)));
        var items = await query.OrderBy(n => n.NocId)
            .Select(n => new { n.NocId, n.NocName })
            .ToListAsync();
        return Ok(items);
    }
}
