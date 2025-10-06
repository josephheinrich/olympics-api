namespace Olympics.Api.Models;

public class Team
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public string NocId { get; set; } = null!;
    public Noc Noc { get; set; } = null!;

    public ICollection<Result> Results { get; set; } = [];
}
