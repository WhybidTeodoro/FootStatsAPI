using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories
{

    /// <summary>
    /// Repositorio de dados para a entidade Player
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {

        private readonly FootDbContext _context;

        public PlayerRepository(FootDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo para adicionar um jogador a um time do usuario no DB
        /// </summary>
        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        /// <summary>
        /// Metodo que retorna uma lista de todos os jogadores de um time do usuario do DB
        /// </summary>
        public async Task<List<Player>> GetAllByTeamAsync(int userId, int teamId)
        {
            return await _context.Players.Where(p => p.Team.UserId == userId && p.TeamId == teamId).ToListAsync();
        }

        /// <summary>
        /// Metodo que retorna um jogador de um time do usario do DB
        /// </summary>
        public async Task<Player?> GetByIdAsync(int userId, int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Team.UserId == userId && p.Id == id);
        }

        /// <summary>
        /// Metodo que verifica se ja existe um jogador com os mesmos dados registrado no DB
        /// </summary>
        public async Task<bool> ExistsAsync(string name, string position, int shirtNumber, int teamId)
        {
            return await _context.Players.AnyAsync(p => p.Name == name
                           && p.Position == position
                           && p.ShirtNumber == shirtNumber
                           && p.TeamId == teamId);
        }

        /// <summary>
        /// Metodo para atualizar um jogador a um time do usuario no DB
        /// </summary>
        public Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Metodo para deletar um jogador a um time do usuario no DB
        /// </summary>
        public Task DeleteAsync(Player player)
        {
            _context.Players.Remove(player);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Metodo para persistir os dados adicionados / alterados no DB
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
