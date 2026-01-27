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
    public string Name { get; set; } = string.Empty;
}
