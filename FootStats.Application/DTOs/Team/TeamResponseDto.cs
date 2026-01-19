using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Team;

/// <summary>
/// dto utilizado para retornar os dados do time
/// </summary>
public class TeamResponseDto
{    
    
    /// <summary>
    /// Identificador unico do time
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do time do usuario
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
