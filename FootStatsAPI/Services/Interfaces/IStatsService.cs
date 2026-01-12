using FootStatsAPI.DTOs.Stats;

namespace FootStatsAPI.Services.Interfaces;

public interface IStatsService
{
    public Task<StatsResponseDto> GetAllStatsByTeam(int userId, int teamId);
}
