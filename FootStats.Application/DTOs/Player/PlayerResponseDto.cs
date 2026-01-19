using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.Player;

public class PlayerResponseDto
{
    /// <summary>
    /// Identificador unico do jogador
    /// </summary>
    public int Id { get; set; }
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
    /// Quantidade de gols do jogador
    /// </summary>
    
    public int Goals { get; set; }

    /// <summary>
    /// Quantidade de assistencias do jogador
    /// </summary>
   
    public int Assists { get; set; }

    /// <summary>
    /// Quantidade de Partidas jogadas pelo jogador
    /// </summary>
  
    public int MatchesPlayed { get; set; }
}
