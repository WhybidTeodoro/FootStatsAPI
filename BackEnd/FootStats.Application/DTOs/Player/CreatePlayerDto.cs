namespace FootStatsAPI.DTOs.Player;

/// <summary>
/// Dto utilizado para adicionar um jogador
/// </summary>
public class CreatePlayerDto
{

    /// <summary>
    /// Nome do jogador
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Posição do jogador
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Numero da camisa
    /// </summary>
    public int ShirtNumber { get; set; }

    /// <summary>
    /// Representa o time do jogador
    /// </summary>
    public int TeamId { get; set; }
}
