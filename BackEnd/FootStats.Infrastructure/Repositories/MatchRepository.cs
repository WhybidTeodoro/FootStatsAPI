using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories;

/// <summary>
/// Repositório de dados para a entidade Match
/// </summary>
public class MatchRepository : IMatchRepository
{
    private readonly FootDbContext _context;

    public MatchRepository(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metodo para adicionar uma partida a um time do usuario no DB
    /// </summary>
    public async Task AddAsync(Match match)
    {
       await _context.Matches.AddAsync(match);
    }

    /// <summary>
    /// Metodo que retorna uma lista com todas as partidas de um time do usuario no DB
    /// </summary>
    public async Task<List<Match>> GetAllMatchesByTeamAsync(int userId, int teamId)
    {
        return await _context.Matches.Where(m => m.Team.UserId == userId && m.TeamId == teamId).ToListAsync();
    }

    /// <summary>
    /// Retorna partidas de um time do usuário com ordenação + paginação aplicadas no banco.
    /// </summary>
    public async Task<PagedResult<Match>> GetPagedByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
    {
        IQueryable<Match> query = _context.Matches
            .AsNoTracking()
            .Where(m => m.Team.UserId == userId && m.TeamId == teamId);

        
        query = ApplyOrdering(query, sorting);
        
        var totalCount = await query.CountAsync();

        var skip = pagination.GetSkipCount();

        var items = await query
            .Skip(skip)
            .Take(pagination.PageSize)
            .ToListAsync();

        
        return PagedResult<Match>.From(
            items: items,
            pageNumber: pagination.PageNumber,
            pageSize: pagination.PageSize,
            totalCount: totalCount);
    }

    /// <summary>
    /// Metodo que retorna uma partida de um time do usuario no DB
    /// </summary>
    public async Task<Match?> GetByIdAsync(int userId, int id)
    {
        return await _context.Matches.FirstOrDefaultAsync(m => m.Team.UserId == userId && m.Id == id);
    }

    /// <summary>
    /// Metodo que atualiza uma partida de um time do usuario no DB
    /// </summary>
    public Task UpdateAsync(Match match)
    {
        _context.Matches.Update(match);
        return Task.CompletedTask;
    }


    /// <summary>
    /// Metodo que deleta uma partida de um time do usuario no DB
    /// </summary>
    public Task DeleteAsync(Match match)
    {
        _context.Matches.Remove(match);
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
    /// Aplica ordenação segura no IQueryable
    /// </summary>
    private static IQueryable<Match> ApplyOrdering(IQueryable<Match> query, SortParameters sorting)
    {
        var sortBy = NormalizeSortBy(sorting.SortBy);

        return sortBy switch
        {
            "matchdate" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(m => m.MatchDate).ThenByDescending(m => m.Id)
                : query.OrderBy(m => m.MatchDate).ThenBy(m => m.Id),

            "opponentteam" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(m => m.OpponentTeam).ThenByDescending(m => m.Id)
                : query.OrderBy(m => m.OpponentTeam).ThenBy(m => m.Id),

            "goalsfor" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(m => m.GoalsFor).ThenByDescending(m => m.Id)
                : query.OrderBy(m => m.GoalsFor).ThenBy(m => m.Id),

            "goalsagainst" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(m => m.GoalsAgainst).ThenByDescending(m => m.Id)
                : query.OrderBy(m => m.GoalsAgainst).ThenBy(m => m.Id),

            _ => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(m => m.Id)
                : query.OrderBy(m => m.Id)
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
