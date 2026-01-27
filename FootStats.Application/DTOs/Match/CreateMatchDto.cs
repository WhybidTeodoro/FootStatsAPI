namespace FootStatsAPI.DTOs.Match;

/// <summary>
/// Dto utilizado para criar partidas
/// </summary>
public class CreateMatchDto
{
    /// <summary>
    /// Data da partida.
    /// </summary>
    public DateOnly MatchDate { get; set; }

    /// <summary>
    /// Nome do time adversario.
    /// </summary>
    public string OpponentTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gols a favor na partida.
    /// </summary>
    public int GoalsFor { get; set; }

    /// <summary>
    /// Gols do adversario na partida.
    /// </summary>
    public int GoalsAgainst { get; set; }

    /// <summary>
    /// Represente o time do usuario
    /// </summary>
    public int TeamId { get; set; }
}
