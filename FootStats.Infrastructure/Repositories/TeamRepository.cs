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

}
