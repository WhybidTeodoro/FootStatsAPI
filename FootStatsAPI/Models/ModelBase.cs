namespace FootStatsAPI.Models;

/// <summary>
/// Classe base para 
/// </summary>
public abstract class ModelBase
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
