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
}
