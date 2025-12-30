using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Player;

public class UpdatePlayerProfileDto
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
}
