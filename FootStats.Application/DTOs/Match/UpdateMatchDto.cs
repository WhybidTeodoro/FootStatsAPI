using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Match;

/// <summary>
/// Dto utilizado para atualizar dados da partida
/// </summary>
public class UpdateMatchDto
{
    /// <summary>
    /// Data da partida.
    /// </summary>
    [Required(ErrorMessage = "A data da partida é obrigatória")]
    public DateOnly MatchDate { get; set; }

    /// <summary>
    /// Nome do time adversario.
    /// </summary>
    [Required(ErrorMessage = "O Nome do time é obrigatório")]
    [MaxLength(50, ErrorMessage = "O Nome do time adversario tem que ter no maximo 50 caracteres")]
    public string OpponentTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gols a favor na partida.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Não é permitido negativos nos gols ")]
    public int GoalsFor { get; set; }

    /// <summary>
    /// Gols do adversario na partida.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "O Numero de gols nao pode ser negativo ")]
    public int GoalsAgainst { get; set; }
}
