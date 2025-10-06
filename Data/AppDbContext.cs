using Microsoft.EntityFrameworkCore;
using Olympics.Api.Models;

namespace Olympics.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");           // dbo.events
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId).HasColumnName("EventId");
            entity.Property(e => e.SportId).HasColumnName("SportId");
            entity.Property(e => e.EventName).HasColumnName("EventName");
        });
    }
}
