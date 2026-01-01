namespace FootStatsAPI.DTOs.Match;

/// <summary>
/// Dto utilizado para retorno de dados da partida
/// </summary>
public class MatchResponseDto
{
    /// <summary>
    /// Identifador unico da partida
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Data da partida.
    /// </summary>
    public DateTime MatchDate { get; set; }

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
}
