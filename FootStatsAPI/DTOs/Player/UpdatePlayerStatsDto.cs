using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Player;

public class UpdatePlayerStatsDto
{
    // <summary>
    /// Quantidade de gols do jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de gols não pode ser negativo")]
    public int Goals { get; set; }

    /// <summary>
    /// Quantidade de assistencias do jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de assistencias não pode ser negativo")]
    public int Assists { get; set; }

    /// <summary>
    /// Quantidade de Partidas jogadas pelo jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de jogos não pode ser negativo")]
    public int MatchesPlayed { get; set; }
}
