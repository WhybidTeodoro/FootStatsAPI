using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

/// <summary>
/// Interface para o repositorio de dados da entidade Match
/// </summary>
public interface IMatchRepository
{

    /// <summary>
    /// Metodo para adicionar uma partida a um time do usuario no DB
    /// </summary>
    Task AddAsync(Match match); 
    
    /// <summary>
    /// Metodo que retorna uma lista com todas as partidas de um time do usuario no DB
    /// </summary>
    Task<List<Match>> GetAllMatchesByTeamAsync(int userId, int teamId);

    /// <summary>
    /// Metodo que retorna uma partida de um time do usuario no DB
    /// </summary>
    Task<Match?> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Metodo que atualiza uma partida de um time do usuario no DB
    /// </summary>
    Task UpdateAsync(Match match);

    /// <summary>
    /// Metodo que deleta uma partida de um time do usuario no DB
    /// </summary>
    Task DeleteAsync(Match match);

    /// <summary>
    /// Metodo para persistir os dados adicionados / alterados no DB
    /// </summary>
    Task SaveChangesAsync();
}
