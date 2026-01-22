using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.DTOs.Stats;
using FootStatsAPI.Services.Interfaces;

namespace FootStatsAPI.Services;

/// <summary>
/// Service responsavel por implementar a regra de negocio de stats
/// </summary>
public class StatsService : IStatsService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMatchRepository _matchRepository;

    public StatsService(ITeamRepository teamRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _matchRepository = matchRepository;
    }

    /// <summary>
    /// Retorna as estatisticas das partidas de um time do usuario
    /// </summary>
    public async Task<StatsResponseDto> GetStatsByTeam(int userId, int teamId)
    {
        var teamExists = await _teamRepository.GetByIdAsync(userId, teamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var matches = await _matchRepository.GetAllMatchesByTeamAsync(userId, teamId);

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
