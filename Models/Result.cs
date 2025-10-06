namespace Olympics.Api.Models;

public class Result
{
    public long ResultId { get; set; }

    public int AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;

    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int? Age { get; set; }
    public int? HeightCm { get; set; }
    public int? WeightKg { get; set; }
    public string? Medal { get; set; } // null | "Gold" | "Silver" | "Bronze"
}
