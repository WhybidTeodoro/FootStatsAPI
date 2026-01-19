using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

public interface ITeamRepository
{
    Task AddAsync(Team team);
    Task<List<Team>> GetAllByUserAsync(int userId);
    Task<Team?> GetByIdAsync(int userId, int teamId);
    Task DeleteAsync(Team team);
    Task SaveChangesAsync();
}
