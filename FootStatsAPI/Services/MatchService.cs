using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;

public class MatchService : IMatchService
{
    private readonly FootDbContext _context;

    public MatchService(FootDbContext context)
    {
        _context = context;
    }

    public async Task<MatchResponseDto> AddMatchAsync(int userId, CreateMatchDto dto)
    {
        var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == dto.TeamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var match = new Match
        {
            OpponentTeam = dto.OpponentTeam,
            MatchDate = dto.MatchDate,
            GoalsFor = dto.GoalsFor,
            GoalsAgainst = dto.GoalsAgainst,
            TeamId = dto.TeamId
        };

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return new MatchResponseDto
        {
            Id = match.Id,
            MatchDate = match.MatchDate,
            OpponentTeam = match.OpponentTeam,
            GoalsFor = match.GoalsFor,
            GoalsAgainst = match.GoalsAgainst,
            TeamId = match.TeamId
        };
    }

    public async Task<MatchResponseDto> GetByIdAsync(int userId, int id)
    {
        var match = await _context.Matches.Where(m => m.Team.UserId == userId && m.Id == id)
            .Select(m => new MatchResponseDto
            {
                Id = m.Id,
                MatchDate = m.MatchDate,
                OpponentTeam = m.OpponentTeam,
                GoalsFor = m.GoalsFor,
                GoalsAgainst = m.GoalsAgainst,
                TeamId = m.TeamId
            }).FirstOrDefaultAsync();

        if (match == null)
            throw new InvalidOperationException("Partida não encontrada");

        return match;
    }

    public Task<MatchResponseDto> UpdateMatchAsync(int userId, int id, UpdateMatchDto dto)
    {
        throw new NotImplementedException();
    }
    
    public Task DeleteMatchAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }
}
