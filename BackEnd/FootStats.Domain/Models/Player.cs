using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.Models;

/// <summary>
/// Representa os jogadores do time.
/// </summary>
public class Player : ModelBase
{

    /// <summary>
    /// Nome do jogador
    /// </summary>
    [Required(ErrorMessage = "O Nome do jogador é obrigatório")]
    [MaxLength(100, ErrorMessage = "O Nome deve ter no maximo 100 caracteres")]
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// Posição do jogador
    /// </summary>
    [Required(ErrorMessage = "A Posição do jogador é obrigatória")]
    [MaxLength(70, ErrorMessage = "A Posição escrita deve ter no maximo 70 caracteres")]
    public required string Position { get; set; } = string.Empty;

    /// <summary>
    /// Numero da camisa
    /// </summary>
    [Range(0, 99, ErrorMessage = "O Numero da camisa é entre 0 e 99")]
    public required int ShirtNumber { get; set; }

    /// <summary>
    /// Quantidade de gols do jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de gols não pode ser negativo")]
    public required int Goals { get; set; }

    /// <summary>
    /// Quantidade de assistencias do jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de assistencias não pode ser negativo")]
    public required int Assists { get; set; }

    /// <summary>
    /// Quantidade de Partidas jogadas pelo jogador
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de jogos não pode ser negativo")]
    public required int MatchesPlayed { get; set; }

    /// <summary>
    /// Representa o time do jogador
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Propriedade de navegação
    /// </summary>
    public Team Team { get; set; } = null!;


}