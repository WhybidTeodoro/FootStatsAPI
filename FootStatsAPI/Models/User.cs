namespace FootStatsAPI.Models;

/// <summary>
/// Entidade que representa o Usuario do sistema
/// </summary>
public class User
{
    /// <summary>
    /// Identificador unico do usuario.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do usuario.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuario. Usado no registro do usuario em sistema.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Hash da senha do usuario. Usado no registro do usuario em sistema.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Data de criação do usuario em sistema.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Lista de times que pertencem ao usuario logado.
    /// </summary>
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}
