using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Stats;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;

/// <summary>
/// Service responsavel por implementar a regra de negocio de stats
/// </summary>
public class StatsService : IStatsService
{
    private readonly FootDbContext _context;

    public StatsService(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna as estatisticas das partidas de um time do usuario
    /// </summary>
    public async Task<StatsResponseDto> GetStatsByTeam(int userId, int teamId)
    {
        var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == teamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var matches = await _context.Matches.Where(m => m.TeamId == teamId).ToListAsync();

        var stats = new StatsResponseDto
        {
            TotalMatches = matches.Count,
            Wins = matches.Count(m => m.GoalsFor > m.GoalsAgainst),
            Losses = matches.Count(m => m.GoalsFor < m.GoalsAgainst),
            Draws = matches.Count(m => m.GoalsFor == m.GoalsAgainst),
            TotalGoalsFor = matches.Sum(m => m.GoalsFor),
            TotalGoalsAgainst = matches.Sum(m => m.GoalsAgainst)
        };

        return stats;
    }
}
