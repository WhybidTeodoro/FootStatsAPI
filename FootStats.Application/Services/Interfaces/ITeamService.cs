using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;

namespace FootStatsAPI.Services.Interfaces;

/// <summary>
/// Interface para implementação das regras de negocio para a entidade Team
/// </summary>
public interface ITeamService
{

    /// <summary>
    /// Adiciona um time ao usuario
    /// </summary>
    Task<TeamResponseDto> AddTeamAsync(int userId, CreateTeamDto dto);

    /// <summary>
    /// Retorna todos os times do usuario
    /// </summary>
    Task<PagedResult<TeamResponseDto>> GetAllAsync(int userId, PaginationParameters pagination, SortParameters sorting);

    /// <summary>
    /// Retorna um time do usuario
    /// </summary>
    Task<TeamResponseDto> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Retorna todos os jogadores de um time do usuario
    /// </summary>
    Task<PagedResult<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting);


    /// <summary>
    /// Retorna todas as partidas de um time do usuario
    /// </summary>
    Task<PagedResult<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting);

    /// <summary>
    /// Atualiza um time do usuario
    /// </summary>
    Task<TeamResponseDto> UpdateTeamAsync(int userId,int id, UpdateTeamDto dto);

    /// <summary>
    /// Deleta um time do usuario
    /// </summary>
    Task DeleteTeamAsync(int userId, int id);
}
