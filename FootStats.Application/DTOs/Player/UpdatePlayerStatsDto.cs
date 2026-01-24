using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Player;

public class UpdatePlayerStatsDto
{
    // <summary>
    /// Quantidade de gols do jogador
    /// </summary>
    public int Goals { get; set; }

    /// <summary>
    /// Quantidade de assistencias do jogador
    /// </summary>
    public int Assists { get; set; }

    /// <summary>
    /// Quantidade de Partidas jogadas pelo jogador
    /// </summary>
    public int MatchesPlayed { get; set; }
}
