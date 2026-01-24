using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

/// <summary>
/// Interface para repositorio de dados da entidade Team
/// </summary>
public interface ITeamRepository
{
    /// <summary>
    /// Metodo para adicionar o time do usuario ao DB
    /// </summary>
    Task AddAsync(Team team);

    /// <summary>
    /// Metodo para retornar todos os times do usuario do DB
    /// </summary>
    Task<List<Team>> GetAllByUserAsync(int userId);

    /// <summary>
    /// Metodo para retornar um time do usuario pelo id do DB
    /// </summary>
    Task<Team?> GetByIdAsync(int userId, int teamId);

    /// <summary>
    /// Metodo para verificar se um time que vai ser adicionado ja existe no DB
    /// </summary>
    Task<bool> ExistsAsync(int userId, string name);

    /// <summary>
    /// Metodo para atualizar um time do usuario no DB
    /// </summary>
    Task UpdateAsync(Team team);

    /// <summary>
    /// Metodo para Deletar um time do usuario no DB
    /// </summary>
    Task DeleteAsync(Team team);

    /// <summary>
    /// Metodo para persistir os dados adicionados / alterados no DB
    /// </summary>
    Task SaveChangesAsync();
}
