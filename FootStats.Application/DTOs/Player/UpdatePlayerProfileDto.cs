namespace FootStatsAPI.DTOs.Player;

public class UpdatePlayerProfileDto
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
}
