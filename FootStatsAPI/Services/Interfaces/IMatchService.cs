using FootStatsAPI.DTOs.Match;

namespace FootStatsAPI.Services.Interfaces;

public interface IMatchService
{
    public Task<MatchResponseDto> AddMatchAsync(int userId, CreateMatchDto dto);

    public Task<MatchResponseDto> GetByIdAsync(int userId, int id);

    public Task<MatchResponseDto> UpdateMatchAsync(int userId, int id, UpdateMatchDto dto);

    public Task DeleteMatchAsync(int userId, int id);
}
