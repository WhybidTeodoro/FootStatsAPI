namespace FootStatsAPI.Models;

public class Match
{
    public int Id { get; set; }
    public DateTime MatchData { get; set; }
    public string OpponentTeam { get; set; } = string.Empty;
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
}
