using FootStatsAPI.DTOs.Stats;

namespace FootStatsAPI.Services.Interfaces;


/// <summary>
/// Interface para implementação da regra de negocio de Stats
/// </summary>
public interface IStatsService
{
    public Task<StatsResponseDto> GetStatsByTeam(int userId, int teamId);
}
