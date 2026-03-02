using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
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
        /// Retorna jogadores de um time do usuário com ordenação + paginação.
        /// </summary>
        public async Task<PagedResult<Player>> GetPagedByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
        {
            IQueryable<Player> query = _context.Players.AsNoTracking().Where(p => p.Team.UserId == userId && p.TeamId == teamId);

            query = ApplyOrdering(query, sorting);

            var totalCount = await query.CountAsync();

            var skip = pagination.GetSkipCount();

            var items = await query.Skip(skip).Take(pagination.PageSize).ToListAsync();

            return PagedResult<Player>.From(items: items, 
                pageNumber: pagination.PageNumber, 
                pageSize: pagination.PageSize, 
                totalCount: totalCount);
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

        /// <summary>
        /// Aplica ordenação segura no IQueryable.
        /// </summary>
        private static IQueryable<Player> ApplyOrdering(IQueryable<Player> query, SortParameters sorting)
        {
            var sortBy = NormalizeSortBy(sorting.SortBy);

            return sortBy switch 
            {
                "name" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.Name).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.Name).ThenBy(p => p.Id),

                "position" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.Position).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.Position).ThenBy(p => p.Id),

                "goals" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.Goals).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.Goals).ThenBy(p => p.Id),

                "assists" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.Assists).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.Assists).ThenBy(p => p.Id),

                "matchesplayed" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.MatchesPlayed).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.MatchesPlayed).ThenBy(p => p.Id),

                "shirtnumber" => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.ShirtNumber).ThenByDescending(p => p.Id)
                    : query.OrderBy(p => p.ShirtNumber).ThenBy(p => p.Id),

                _ => sorting.Direction == SortDirection.Desc
                    ? query.OrderByDescending(p => p.Id)
                    : query.OrderBy(p => p.Id)
            };
        }

        /// <summary>
        /// Normaliza SortBy
        /// </summary>
        private static string NormalizeSortBy(string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return "id";

            return sortBy.Trim().ToLowerInvariant();
        }
    }
}
