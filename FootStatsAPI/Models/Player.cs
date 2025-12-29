using FootStatsAPI.Models;

/// <summary>
/// Representa os jogadores do time.
/// </summary>
public class Player
{
    /// <summary>
    /// Identificador unico do jogador
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Nome do jogador
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Posição do jogador
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Numero da camisa
    /// </summary>
    public int ShirtNumber { get; set; }

    /// <summary>
    /// Quantidade de gols do jogador
    /// </summary>
    public int Goals { get; set; }

    /// <summary>
    /// Quantidade de assistencias do jogador
    /// </summary>
    public int Assist { get; set; }

    /// <summary>
    /// Quantidade de Partidas jogadas pelo jogador
    /// </summary>
    public int MatchesPlayed { get; set; }

    /// <summary>
    /// Representa o time do jogador
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Propriedade de navegação
    /// </summary>
    public Team Team { get; set; } = null!;


}