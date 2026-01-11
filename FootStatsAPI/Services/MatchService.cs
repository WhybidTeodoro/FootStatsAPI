using FootStatsAPI.DTOs.Match;
using FootStatsAPI.Services.Interfaces;

namespace FootStatsAPI.Services;

public class MatchService : IMatchService
{
    public Task<MatchResponseDto> AddMatchAsync(int userId, CreateMatchDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<MatchResponseDto> GetByIdAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public Task<MatchResponseDto> UpdateMatchAsync(int userId, int id, UpdateMatchDto dto)
    {
        throw new NotImplementedException();
    }
    
    public Task DeleteMatchAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }
}
