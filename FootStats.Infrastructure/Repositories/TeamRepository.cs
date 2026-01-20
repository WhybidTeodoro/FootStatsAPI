using FootStats.Application.Services.Interfaces.Repositories;
using FootStats.Infrastructure.Data;
using FootStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootStats.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{

    private readonly FootDbContext _context;

    public TeamRepository(FootDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }
    
    public async Task<List<Team>> GetAllByUserAsync(int userId)
    {
        return await _context.Teams.Where(t => t.UserId == userId).ToListAsync();
    }
    
    public async Task<Team?> GetByIdAsync(int userId, int teamId)
    {
        return await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == teamId);
    }

    public Task DeleteAsync(Team team)
    {
        _context.Teams.Remove(team);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int userId, string name)
    {
        return await _context.Teams.AnyAsync(t => t.UserId == userId && t.Name == name);
    }

    public Task UpdateAsync(Team team)
    {
        _context.Teams.Update(team);
        return Task.CompletedTask;
    }
}
