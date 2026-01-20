using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

public interface ITeamRepository
{
    Task AddAsync(Team team);
    Task<List<Team>> GetAllByUserAsync(int userId);
    Task<Team?> GetByIdAsync(int userId, int teamId);
    Task<bool> ExistsAsync(int userId, string name);
    Task UpdateAsync(Team team);
    Task DeleteAsync(Team team);
    Task SaveChangesAsync();
}
