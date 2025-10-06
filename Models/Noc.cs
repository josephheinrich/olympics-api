namespace Olympics.Api.Models;

public class Noc
{
    public string NocId { get; set; } = null!;
    public string? NocName { get; set; }

    public ICollection<Team> Teams { get; set; } = [];
}
