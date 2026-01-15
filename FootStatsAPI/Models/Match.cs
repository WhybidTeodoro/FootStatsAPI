using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.Models;

/// <summary>
/// Representa as partidas do time do usuario
/// </summary>
public class Match : ModelBase
{
    /// <summary>
    /// Data da partida.
    /// </summary>
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
    public required int GoalsAgainst { get; set; }

    /// <summary>
    /// Represente o time do usuario
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Propriedade de navegação
    /// </summary>
    public Team Team { get; set; } = null!;
}
