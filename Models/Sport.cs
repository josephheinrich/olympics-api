namespace Olympics.Api.Models;

public class Sport
{
    public int SportId { get; set; }
    public string SportName { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = [];
}
