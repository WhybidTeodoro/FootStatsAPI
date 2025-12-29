
using FootStatsAPI.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int ShirtNumber { get; set; }
    public int Goals { get; set; }
    public int Assist { get; set; }
    public int MatchesPlayed { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;


}