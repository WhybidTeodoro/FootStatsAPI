using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly FootDbContext _context;

    public MatchRepository(FootDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Match match)
    {
       await _context.Matches.AddAsync(match);
    }

    public Task DeleteAsync(Match match)
    {
        _context.Matches.Remove(match);
        return Task.CompletedTask;
    }

    public async Task<List<Match>> GetAllMatchesByTeamAsync(int userId, int teamId)
    {
        return await _context.Matches.Where(m => m.Team.UserId == userId && m.TeamId == teamId).ToListAsync();
    }

    public async Task<Match?> GetByIdAsync(int userId, int id)
    {
        return await _context.Matches.FirstOrDefaultAsync(m => m.Team.UserId == userId && m.Id == id);
    }

    public Task UpdateAsync(Match match)
    {
        _context.Matches.Update(match);
        return Task.CompletedTask;
    }
}
