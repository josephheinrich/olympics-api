namespace Olympics.Api.Models;

public class Event
{
    public int EventId { get; set; }
    public int SportId { get; set; }
    public Sport Sport { get; set; } = null!;
    public string EventName { get; set; } = null!;

    public ICollection<Result> Results { get; set; } = [];
}
