namespace FootStatsAPI.DTOs.Stats;

/// <summary>
/// Dto utilizado para retornar estatisticas de um time
/// </summary>
public class StatsResponseDto
{
    /// <summary>
    /// Total de partidas disputadas
    /// </summary>
    public int TotalMatches { get; set; }

    /// <summary>
    /// Total de vitorias
    /// </summary>
    public int Wins { get; set; }
    
    /// <summary>
    /// Total de derrotas
    /// </summary>
    public int Losses { get; set; }

    /// <summary>
    /// Total de empates
    /// </summary>
    public int Draws { get; set; }

    /// <summary>
    /// Total de gols feitos.
    /// </summary>
    public int TotalGoalsFor { get; set; }

    /// <summary>
    /// Total de gols tomados.
    /// </summary>
    public int TotalGoalsAgainst { get; set; }

    /// <summary>
    /// Saldo de gols
    /// </summary>
    public int GoalDifference { get; set; }
}
