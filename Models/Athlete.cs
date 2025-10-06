namespace Olympics.Api.Models;

public class Athlete
{
    public int AthleteId { get; set; }            // CSV ID
    public string AthleteName { get; set; } = null!;
    public char Sex { get; set; }                 // 'M'|'F'

    public ICollection<Result> Results { get; set; } = [];
}
