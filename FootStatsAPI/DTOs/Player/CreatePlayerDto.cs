using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Player;

/// <summary>
/// Dto utilizado para adicionar um jogador
/// </summary>
public class CreatePlayerDto
{

    /// <summary>
    /// Nome do jogador
    /// </summary>
    [Required(ErrorMessage = "O Nome do jogador é obrigatório")]
    [MaxLength(100, ErrorMessage = "O Nome deve ter no maximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Posição do jogador
    /// </summary>
    [Required(ErrorMessage = "A Posição do jogador é obrigatória")]
    [MaxLength(70, ErrorMessage = "A Posição escrita deve ter no maximo 70 caracteres")]
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Numero da camisa
    /// </summary>
    [Range(0, 99, ErrorMessage = "O Numero da camisa é entre 0 e 99")]
    public int ShirtNumber { get; set; }

    /// <summary>
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

    /// <summary>
    /// Data da criação do player em sistema
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Representa o time do jogador
    /// </summary>
    public int TeamId { get; set; }
}
