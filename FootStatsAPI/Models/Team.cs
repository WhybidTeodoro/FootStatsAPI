using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.Models;


/// <summary>
/// Representa o time do usuario.
/// </summary>
public class Team : ModelBase
{

    /// <summary>
    /// Nome do time do usuario
    /// </summary>
    [Required(ErrorMessage = "O Nome do time é obrigatório")]
    [MaxLength(50, ErrorMessage = "O Nome do time tem que ter no maximo 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Representa o usuario 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///Propriedade de navegação 
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Lista de jogadores que pertencem ao time.
    /// </summary>
    public ICollection<Player> Players { get; set; } = new List<Player>();

    /// <summary>
    /// Lista de partidas que pertencem ao time
    /// </summary>
    public ICollection<Match> Matches { get; set; } = new List<Match>();


}