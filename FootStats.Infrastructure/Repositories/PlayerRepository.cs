using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string name, string position, int shirtNumber, int teamId)
        {
            return await _context.Players.AnyAsync(p => p.Name == name
                           && p.Position == position
                           && p.ShirtNumber == shirtNumber
                           && p.TeamId == teamId);
        }
    }
}
