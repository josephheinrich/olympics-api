using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default")
           ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");

builder.Services.AddDbContext<AppDb>(opt =>
    opt.UseSqlServer(conn));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/events", async (AppDb db) =>
{
    var data = await db.Events
        .Select(e => new EventDto(e.Id, e.EventName))
        .ToListAsync();

    return Results.Ok(data);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
