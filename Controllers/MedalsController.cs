using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olympics.Api.Data;
using Olympics.Api.Dtos;
using AutoMapper;

namespace Olympics.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class MedalsController(AppDbContext db, IMapper mapper) : ControllerBase
{
    // // GET /api/v1/medals/table?year=2016&season=Summer
    // [HttpGet("table")]
    // public async Task<ActionResult<IEnumerable<NocMedalDto>>> Table([FromQuery] int year, [FromQuery] string season)
    // {
    //     if (string.IsNullOrWhiteSpace(season)) return BadRequest("season is required: Summer|Winter");

    //     var rows = await db.Results.AsNoTracking()
    //         .Where(r => r.Game.Year == year && r.Game.Season == season && r.Medal != null)
    //         .GroupBy(r => r.Team.NocId)
    //         .Select(g => new NocMedalDto(
    //             Noc: g.Key,
    //             Gold: g.Count(x => x.Medal == "Gold"),
    //             Silver: g.Count(x => x.Medal == "Silver"),
    //             Bronze: g.Count(x => x.Medal == "Bronze"),
    //             Total: g.Count()))
    //         .OrderByDescending(x => x.Gold)
    //         .ThenByDescending(x => x.Silver)
    //         .ThenByDescending(x => x.Bronze)
    //         .ToListAsync();

    //     return Ok(rows);
    // }

    // // GET /api/v1/medals/noc-trend?noc=USA&season=Summer
    // [HttpGet("noc-trend")]
    // public async Task<ActionResult<IEnumerable<NocTrendPoint>>> NocTrend([FromQuery] string noc, [FromQuery] string? season)
    // {
    //     if (string.IsNullOrWhiteSpace(noc)) return BadRequest("noc is required");

    //     var q = db.Results.AsNoTracking()
    //         .Where(r => r.Team.NocId == noc.ToUpperInvariant() && r.Medal != null);

    //     if (!string.IsNullOrWhiteSpace(season))
    //         q = q.Where(r => r.Game.Season == season);

    //     var data = await q
    //         .GroupBy(r => new { r.Game.Year, r.Game.Season })
    //         .Select(g => new NocTrendPoint(
    //             Year: g.Key.Year,
    //             Season: g.Key.Season,
    //             Gold: g.Count(x => x.Medal == "Gold"),
    //             Silver: g.Count(x => x.Medal == "Silver"),
    //             Bronze: g.Count(x => x.Medal == "Bronze"),
    //             Total: g.Count()))
    //         .OrderBy(x => x.Year).ThenBy(x => x.Season)
    //         .ToListAsync();

    //     return Ok(data);
    // }
}
