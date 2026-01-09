using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;

public class TeamService : ITeamServices
{

    private readonly FootDbContext _context;

    public TeamService(FootDbContext context)
    {
        _context = context;
    }

    public async Task<TeamResponseDto> AddTeamAsync(int userId, CreateTeamDto dto)
    {
        var teamExists = await _context.Teams.AnyAsync(t => t.UserId == userId && t.Name == dto.Name);

        if (teamExists)
            throw new InvalidOperationException("O time já existe");

        try
        {
            var team = new Team
            {
                UserId = userId,
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name
            };
        }
        catch (Exception ex)
        {

            throw new Exception("Erro ao Adicionar time", ex);
        }
    }

    public Task<TeamResponseDto> DeleteTeamAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public async Task <List<TeamResponseDto>> GetAllAsync(int userId)
    {
            return await _context.Teams.Where(t => t.UserId == userId)
           .Select(teams => new TeamResponseDto
           {
               Id = teams.Id,
               Name = teams.Name
           }).ToListAsync();
    }

    public async Task<List<MatchResponseDto>> GetAllMatchessByTeamAsync(int userId, int teamId)
    {
        var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId && t.UserId == userId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        return await _context.Matches.Where(m => m.TeamId == teamId && m.Team.UserId == userId)
            .Select(m => new MatchResponseDto
            {
                Id = m.Id,
                MatchDate = m.MatchDate,
                OpponentTeam = m.OpponentTeam,
                GoalsFor = m.GoalsFor,
                GoalsAgainst = m.GoalsAgainst
            }).ToListAsync();
    }

    public async Task<List<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId)
    {

        var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId && t.UserId == userId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        return await _context.Players.Where(p => p.TeamId == teamId && p.Team.UserId == userId)
                    .Select(p => new PlayerResponseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Position = p.Position,
                        ShirtNumber = p.ShirtNumber,
                        MatchesPlayed = p.MatchesPlayed,
                        Goals = p.Goals,
                        Assists = p.Assists
                    }).ToListAsync();
    }

    public async Task<TeamResponseDto> GetByIdAsync(int userId, int id)
    {
        var team =  await _context.Teams.Where(t => t.Id == id && t.UserId == userId)
                    .Select(t => new TeamResponseDto
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).FirstOrDefaultAsync();

        return (team!);
    }

    public Task<TeamResponseDto> UpdateTeamAsync(int userId, UpdateTeamDto dto)
    {
        throw new NotImplementedException();
    }
}
