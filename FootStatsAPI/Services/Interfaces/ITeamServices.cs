using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;

namespace FootStatsAPI.Services.Interfaces;

/// <summary>
/// Interface para implementação das regras de negocio para a entidade Team
/// </summary>
public interface ITeamServices
{

    /// <summary>
    /// Adiciona um time ao usuario
    /// </summary>
    Task<TeamResponseDto> AddTeamAsync(int userId, CreateTeamDto dto);

    /// <summary>
    /// Retorna todos os times do usuario
    /// </summary>
    Task<List<TeamResponseDto>> GetAllAsync(int userId);

    /// <summary>
    /// Retorna um time do usuario
    /// </summary>
    Task<TeamResponseDto> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Retorna todos os jogadores de um time do usuario
    /// </summary>
    Task<List<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId);

    /// <summary>
    /// Retorna todas as partidas de um time do usuario
    /// </summary>
    Task<List<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId);

    /// <summary>
    /// Atualiza um time do usuario
    /// </summary>
    Task<TeamResponseDto> UpdateTeamAsync(int userId,int id, UpdateTeamDto dto);

    /// <summary>
    /// Deleta um time do usuario
    /// </summary>
    Task DeleteTeamAsync(int userId, int id);
}
