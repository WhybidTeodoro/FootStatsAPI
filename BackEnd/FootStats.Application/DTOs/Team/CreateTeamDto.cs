using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Team;

/// <summary>
/// Dto utilizado para criação do time
/// </summary>
public class CreateTeamDto
{
    /// <summary>
    /// Nome do time do usuario
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
