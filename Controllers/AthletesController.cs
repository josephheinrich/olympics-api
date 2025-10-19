using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olympics.Api.Data;
using Olympics.Api.Dtos;
using Olympics.Api.Infrastructure;

namespace Olympics.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AthletesController(AppDbContext db, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Paged<AthleteDto>>> Search(
        [FromQuery] string? name,
        [FromQuery] string? sex,
        [FromQuery] string? noc,
        [FromQuery] Query q)
    {
        var query = db.Athletes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(a => a.AthleteName.Contains(name));

        if (!string.IsNullOrWhiteSpace(sex))
        {
            var s = char.ToUpperInvariant(sex[0]);
            if (s is 'M' or 'F')
                query = query.Where(a => a.Sex == s);
        }

        if (!string.IsNullOrWhiteSpace(noc))
        {
            var code = noc.Trim().ToUpperInvariant();
            query = query.Where(a => a.Results.Any(r => r.Team.NocId == code));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(a => a.AthleteName)
            .Skip(q.Skip).Take(q.Take)
            .ProjectTo<AthleteDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(new Paged<AthleteDto>(q.Page, q.PageSize, total, items));
    }

    [HttpGet("{athleteId:int}")]
    public async Task<ActionResult<AthleteDto>> Get(int athleteId)
    {
        var dto = await db.Athletes.AsNoTracking()
            .Where(a => a.AthleteId == athleteId)
            .ProjectTo<AthleteDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("{athleteId:int}/results")]
    public async Task<ActionResult<IEnumerable<ResultDto>>> Results(
        int athleteId,
        [FromQuery] string? medal)
    {
        var query = db.Results.AsNoTracking()
            .Include(r => r.Athlete)
            .Include(r => r.Team).ThenInclude(t => t.Noc)
            .Include(r => r.Event).ThenInclude(e => e.Sport)
            .Include(r => r.Game)
            .Where(r => r.AthleteId == athleteId);

        if (!string.IsNullOrWhiteSpace(medal))
        {
            var m = char.ToUpperInvariant(medal[0]) switch
            {
                'G' => "Gold",
                'S' when medal.StartsWith("si", StringComparison.OrdinalIgnoreCase) => "Silver",
                'B' => "Bronze",
                _ => null
            };
            if (m is not null) query = query.Where(r => r.Medal == m);
        }

        var items = await query
            .OrderBy(r => r.Game.Year)
            .ThenBy(r => r.Event.EventName)
            .ProjectTo<ResultDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(items);
    }
}
