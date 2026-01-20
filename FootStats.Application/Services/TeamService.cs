using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;


namespace FootStatsAPI.Services;

/// <summary>
/// Service que implementa a regra de negocio para a entidade Team
/// </summary>
public class TeamService : ITeamService
{

    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;

    public TeamService(ITeamRepository teamRepository, IPlayerRepository playerRepository)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
    }

    /// <summary>
    /// Adiciona um time ao usuario
    /// </summary>
    public async Task<TeamResponseDto> AddTeamAsync(int userId,CreateTeamDto dto)
    {
        var teamExists = await _teamRepository.ExistsAsync(userId, dto.Name);

        if (teamExists)
            throw new InvalidOperationException("O time já existe");

            var team = new Team
            {
                UserId = userId,
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _teamRepository.AddAsync(team);
            await _teamRepository.SaveChangesAsync();

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
        var teams = await _teamRepository.GetAllByUserAsync(userId);

       return teams.Select(teams => new TeamResponseDto
       {
           Id = teams.Id,
           Name = teams.Name
       }).ToList();
    }
    
    /// <summary>
    /// Retorna um time do usuario
    /// </summary>
    public async Task<TeamResponseDto> GetByIdAsync(int userId, int teamid)
    {
        var team = await _teamRepository.GetByIdAsync(userId, teamid);
        
        if (team == null)
            throw new InvalidOperationException("Time não existe");

        return new TeamResponseDto
        {
            Id = team.Id,
            Name = team.Name
        };
    }

    /// <summary>
    /// Retorna todos os jogadores de um time do usuario
    /// </summary>
    public async Task<List<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId)
    {

        var team = await _teamRepository.GetByIdAsync(userId, teamId);

        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        var player = await _playerRepository.GetAllByTeamAsync(userId, teamId);

        return player.Select(p => new PlayerResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Position = p.Position,
            ShirtNumber = p.ShirtNumber,
            Goals = p.Goals,
            Assists = p.Assists,
            MatchesPlayed = p.MatchesPlayed
        }).ToList();
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
    public async Task<TeamResponseDto> UpdateTeamAsync(int userId, int teamid, UpdateTeamDto dto)
    {
        var team = await _teamRepository.GetByIdAsync(userId, teamid);

        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

            team.Name = dto.Name;
            team.UpdatedAt = DateTime.UtcNow;

            await _teamRepository.UpdateAsync(team);
            await _teamRepository.SaveChangesAsync();

            return new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name
            };
    }

    /// <summary>
    /// Deleta um time do usuario
    /// </summary>
    public async Task DeleteTeamAsync(int userId, int teamid)
    {
        var team = await _teamRepository.GetByIdAsync(userId, teamid);

        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        await _teamRepository.DeleteAsync(team);
        await _teamRepository.SaveChangesAsync();
    }
}