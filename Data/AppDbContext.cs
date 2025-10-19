using Microsoft.EntityFrameworkCore;
using Olympics.Api.Models;

namespace Olympics.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Noc> Nocs => Set<Noc>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Sport> Sports => Set<Sport>();
    public DbSet<Models.Event> Events => Set<Models.Event>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<Result> Results => Set<Result>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Noc>(e =>
        {
            e.ToTable("Noc");
            e.HasKey(x => x.NocId);
            e.Property(x => x.NocId).HasMaxLength(3).IsFixedLength();
            e.Property(x => x.NocName).HasMaxLength(100);
        });

        mb.Entity<Team>(e =>
        {
            e.ToTable("Team");
            e.HasKey(x => x.TeamId);
            e.Property(x => x.TeamName).HasMaxLength(150).IsRequired();
            e.Property(x => x.NocId).HasMaxLength(3).IsFixedLength().IsRequired();
            e.HasOne(x => x.Noc).WithMany(n => n.Teams).HasForeignKey(x => x.NocId);
            e.HasIndex(x => new { x.TeamName, x.NocId }).IsUnique();
        });

        mb.Entity<Sport>(e =>
        {
            e.ToTable("Sport");
            e.HasKey(x => x.SportId);
            e.Property(x => x.SportName).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.SportName).IsUnique();
        });

        mb.Entity<Models.Event>(e =>
        {
            e.ToTable("Event");
            e.HasKey(x => x.EventId);
            e.Property(x => x.EventName).HasMaxLength(200).IsRequired();
            e.HasOne(x => x.Sport).WithMany(s => s.Events).HasForeignKey(x => x.SportId);
            e.HasIndex(x => new { x.SportId, x.EventName }).IsUnique();
        });

        mb.Entity<Game>(e =>
        {
            e.ToTable("Game");
            e.HasKey(x => x.GameId);
            e.Property(x => x.Year).IsRequired();
            e.Property(x => x.Season).HasMaxLength(10).IsRequired();
            e.Property(x => x.City).HasMaxLength(100).IsRequired();
            e.Property(x => x.GamesLabel).HasMaxLength(30).IsRequired();
            // Allow multi-city editions (e.g., 1956 Summer)
            e.HasIndex(x => new { x.Year, x.Season, x.City }).IsUnique()
             .HasDatabaseName("UQ_Game_Year_Season_City");
        });

        mb.Entity<Athlete>(e =>
        {
            e.ToTable("Athlete");
            e.HasKey(x => x.AthleteId);
            e.Property(x => x.AthleteName).HasMaxLength(200).IsRequired();
            e.Property(x => x.Sex).HasColumnType("char(1)").IsRequired();
        });

        mb.Entity<Result>(e =>
        {
            e.ToTable("Result");
            e.HasKey(x => x.ResultId);

            e.HasOne(x => x.Athlete).WithMany(a => a.Results).HasForeignKey(x => x.AthleteId);
            e.HasOne(x => x.Team).WithMany(t => t.Results).HasForeignKey(x => x.TeamId);
            e.HasOne(x => x.Game).WithMany(g => g.Results).HasForeignKey(x => x.GameId);
            e.HasOne(x => x.Event).WithMany(ev => ev.Results).HasForeignKey(x => x.EventId);

            e.Property(x => x.Medal).HasMaxLength(6);
            e.HasIndex(x => x.GameId);
            e.HasIndex(x => x.EventId);
            e.HasIndex(x => x.TeamId);
            e.HasIndex(x => x.AthleteId);
            e.HasIndex(x => x.Medal).HasFilter("[Medal] IS NOT NULL");
        });
    }
}
