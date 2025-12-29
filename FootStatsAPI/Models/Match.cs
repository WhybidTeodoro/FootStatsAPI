namespace FootStatsAPI.Models;

/// <summary>
/// Representa as partidas do time do usuario
/// </summary>
public class Match
{
    /// <summary>
    /// Identifador unico da partida
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Data da partida.
    /// </summary>
    public DateTime MatchData { get; set; }

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

    /// <summary>
    /// Propriedade de navegação
    /// </summary>
    public Team Team { get; set; } = null!;
}
