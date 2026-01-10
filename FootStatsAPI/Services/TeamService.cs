using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;

/// <summary>
/// Service que implementa a regra de negocio para a entidade Team
/// </summary>
public class TeamService : ITeamService
{

    private readonly FootDbContext _context;

    public TeamService(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona um time ao usuario
    /// </summary>
    public async Task<TeamResponseDto> AddTeamAsync(int userId, CreateTeamDto dto)
    {
        var teamExists = await _context.Teams.AnyAsync(t => t.UserId == userId && t.Name == dto.Name);

        if (teamExists)
            throw new InvalidOperationException("O time já existe");

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
    
    /// <summary>
    /// Retorna todos os times do usuario
    /// </summary>
    public async Task<List<TeamResponseDto>> GetAllAsync(int userId)
    {
        return await _context.Teams.Where(t => t.UserId == userId)
       .Select(teams => new TeamResponseDto
       {
           Id = teams.Id,
           Name = teams.Name
       }).ToListAsync();
    }
    
    /// <summary>
    /// Retorna um time do usuario
    /// </summary>
    public async Task<TeamResponseDto> GetByIdAsync(int userId, int id)
    {
        var team = await _context.Teams.Where(t => t.Id == id && t.UserId == userId)
                    .Select(t => new TeamResponseDto
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).FirstOrDefaultAsync();

        if (team == null)
            throw new InvalidOperationException("Time não existe");

        return team;
    }

    /// <summary>
    /// Retorna todos os jogadores de um time do usuario
    /// </summary>
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
    
    /// <summary>
    /// Retorna todas as partidas de um time do usuario
    /// </summary>
    public async Task<List<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId)
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

    /// <summary>
    /// Atualiza um time do usuario
    /// </summary>
    public async Task<TeamResponseDto> UpdateTeamAsync(int userId, int id, UpdateTeamDto dto)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

            team.Name = dto.Name;

            await _context.SaveChangesAsync();

            return new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name
            };

    }

    /// <summary>
    /// Deleta um time do usuario
    /// </summary>
    public async Task DeleteTeamAsync(int userId, int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);

        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
    }
}