using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly FootDbContext _context;

        public PlayerRepository(FootDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        public Task DeleteAsync(Player player)
        {
            _context.Players.Remove(player);
            return Task.CompletedTask;
        }

        public async Task<List<Player>> GetAllByTeamAsync(int userId, int teamId)
        {
            return await _context.Players.Where(p => p.Team.UserId == userId && p.TeamId == teamId).ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(int userId, int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Team.UserId == userId && p.Id == id);
        }

        public async Task<bool> PlayerExists(Player player)
        {
            return await _context.Players.AnyAsync(p => p.Name == player.Name
                            && p.Position == player.Position 
                            && p.ShirtNumber == player.ShirtNumber 
                            && p.TeamId == player.TeamId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            return Task.CompletedTask;
        }
    }
}
