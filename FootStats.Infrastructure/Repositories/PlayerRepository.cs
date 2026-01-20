using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.Models;

namespace FootStats.Infrastructure.Repositories
{
    internal class PlayerRepository : IPlayerRepository
    {
        public Task AddAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> GetAllByTeamAsync(int userId, int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<Player?> GetByIdAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
