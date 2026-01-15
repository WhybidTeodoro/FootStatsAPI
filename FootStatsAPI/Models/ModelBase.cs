namespace FootStatsAPI.Models;

/// <summary>
/// Classe base para 
/// </summary>
public abstract class ModelBase
{
    /// <summary>
    /// Identificador unico
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Data da criação em sistema
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Data da atualização em sistema
    /// </summary>
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
