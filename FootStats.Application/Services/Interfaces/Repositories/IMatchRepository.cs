using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

public interface IMatchRepository
{
    Task<Match> AddAsync(Match match);
    Task<Match?> GetByIdAsync(int userId, int id);
    Task<List<Match>> GetAllMatchesByTeamAsync(int userId, int teamId);
    Task UpdateAsync(Match match);
    Task DeleteAsync(Match match);
}
