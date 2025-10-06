using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olympics.Api.Data;
using Olympics.Api.Dtos;
using Olympics.Api.Models;

namespace Olympics.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(AppDbContext db, IMapper mapper) : ControllerBase
{
    // GET: api/events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
    {
        var entities = await db.Events.AsNoTracking().ToListAsync();
        var dtos = mapper.Map<List<EventDto>>(entities);
        return Ok(dtos);
    }

    // GET: api/events/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventDto>> GetById(int id)
    {
        var entity = await db.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null) return NotFound();
        return Ok(mapper.Map<EventDto>(entity));
    }

    // (Optional) POST scaffold for later:
    // [HttpPost]
    // public async Task<ActionResult<EventDto>> Create(EventCreateDto input) { ... }

    // (Optional) PUT/PATCH/DELETE scaffolds for later...
}
