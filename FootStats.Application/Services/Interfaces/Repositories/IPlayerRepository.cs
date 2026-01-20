using FootStatsAPI.Models;

namespace FootStats.Application.Services.Interfaces.Repositories;

public interface IPlayerRepository
{
    Task AddAsync(Player player);
    Task<List<Player>> GetAllByTeamAsync(int userId, int teamId);
    Task<Player?> GetByIdAsync(int userId, int id);
    Task<bool> ExistsAsync(string name, string position, int shirtNumber, int teamId);
    Task UpdateAsync(Player player);
    Task DeleteAsync(Player player);
    Task SaveChangesAsync();
}
