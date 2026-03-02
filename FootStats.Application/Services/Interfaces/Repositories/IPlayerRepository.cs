using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

/// <summary>
/// Interface para o repositorio de dados da entidade Player
/// </summary>
public interface IPlayerRepository
{

    /// <summary>
    /// Metodo para adicionar um jogador a um time do usuario no DB
    /// </summary>
    Task AddAsync(Player player);

    /// <summary>
    /// Metodo que retorna uma lista de todos os jogadores de um time do usuario do DB
    /// </summary>
    Task<List<Player>> GetAllByTeamAsync(int userId, int teamId);

    /// <summary>
    /// Metodo que retorna uma lista de todos os jogadores de um time do usuario do DB
    /// </summary>
    Task<PagedResult<Player>> GetPagedByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting);

    /// <summary>
    /// Metodo que retorna um jogador de um time do usario do DB
    /// </summary>
    Task<Player?> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Metodo que verifica se ja existe um jogador com os mesmos dados registrado no DB
    /// </summary>
    Task<bool> ExistsAsync(string name, string position, int shirtNumber, int teamId);

    /// <summary>
    /// Metodo para atualizar um jogador a um time do usuario no DB
    /// </summary>
    Task UpdateAsync(Player player);

    /// <summary>
    /// Metodo para deletar um jogador a um time do usuario no DB
    /// </summary>
    Task DeleteAsync(Player player);

    /// <summary>
    /// Metodo para persistir os dados adicionados / alterados no DB
    /// </summary>
    Task SaveChangesAsync();
}
