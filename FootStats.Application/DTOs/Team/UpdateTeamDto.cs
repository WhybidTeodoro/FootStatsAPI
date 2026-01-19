using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Team;

/// <summary>
/// Dto utilizado para atualizar um time existente do usuario.
/// </summary>
public class UpdateTeamDto
{
    /// <summary>
    /// Nome do time do usuario.
    /// </summary>
    [Required(ErrorMessage = "O Nome do time é obrigatório")]
    [MaxLength(50, ErrorMessage = "O Nome do time tem que ter no maximo 50 caracteres")]
    public string Name { get; set; } = string.Empty;
}
