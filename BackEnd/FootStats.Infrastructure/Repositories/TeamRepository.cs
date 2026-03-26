using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories;

/// <summary>
/// Repositorio de dados para a entidade Team
/// </summary>
public class TeamRepository : ITeamRepository
{

    private readonly FootDbContext _context;

    public TeamRepository(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metodo para adicionar o time do usuario ao DB
    /// </summary>
    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }

    /// <summary>
    /// Metodo para retornar todos os times do usuario do DB
    /// </summary>
    public async Task<List<Team>> GetAllByUserAsync(int userId)
    {
        return await _context.Teams.Where(t => t.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Retorna times do usuário com ordenação + paginação aplicadas no banco.
    /// </summary>
    public async Task<PagedResult<Team>> GetPagedByUserAsync(int userId, PaginationParameters pagination, SortParameters sorting)
    {
        IQueryable<Team> query = _context.Teams.AsNoTracking().Where(t => t.UserId == userId);

        query = ApplyOrdering(query, sorting);

        var totalCount = await query.CountAsync();

        var skip = pagination.GetSkipCount();

        var items = await query.Skip(skip).Take(pagination.PageSize).ToListAsync();

        return PagedResult<Team>.From(items : items, pageNumber : pagination.PageNumber, pageSize: pagination.PageSize, totalCount:  totalCount);
    }

    /// <summary>
    /// Metodo para retornar um time do usuario pelo id do DB
    /// </summary>
    public async Task<Team?> GetByIdAsync(int userId, int teamId)
    {
        return await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == teamId);
    }



    /// <summary>
    /// Metodo para verificar se um time que vai ser adicionado ja existe no DB
    /// </summary>
    public async Task<bool> ExistsAsync(int userId, string name)
    {
        return await _context.Teams.AnyAsync(t => t.UserId == userId && t.Name == name);
    }

    /// <summary>
    /// Metodo para atualizar um time do usuario no DB
    /// </summary>
    public Task UpdateAsync(Team team)
    {
        _context.Teams.Update(team);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Metodo para persistir os dados adicionados / alterados no DB
    /// </summary>
    public Task DeleteAsync(Team team)
    {
        _context.Teams.Remove(team);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Metodo para persistir os dados no DB
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Aplica ordenação segura (whitelist) no IQueryable.
    /// </summary>
    private static IQueryable<Team> ApplyOrdering(IQueryable<Team> query, SortParameters sorting)
    {
        
        var sortBy = NormalizeSortBy(sorting.SortBy);

        return sortBy switch
        {
            "name" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(t => t.Name).ThenByDescending(t => t.Id)
                : query.OrderBy(t => t.Name).ThenBy(t => t.Id),

            "createdat" => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(t => t.CreatedAt).ThenByDescending(t => t.Id)
                : query.OrderBy(t => t.CreatedAt).ThenBy(t => t.Id),

            _ => sorting.Direction == SortDirection.Desc
                ? query.OrderByDescending(t => t.Id)
                : query.OrderBy(t => t.Id)
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
