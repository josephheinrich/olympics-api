namespace Olympics.Api.Models;

public class Game
{
    public int GameId { get; set; }
    public int Year { get; set; }
    public string Season { get; set; } = null!; // "Summer" | "Winter"
    public string City { get; set; } = null!;
    public string GamesLabel { get; set; } = null!;

    public ICollection<Result> Results { get; set; } = [];
}
