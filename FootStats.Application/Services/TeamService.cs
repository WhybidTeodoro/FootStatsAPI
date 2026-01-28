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
    private readonly IMatchRepository _matchRepository;

    public TeamService(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _matchRepository = matchRepository;
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

            return MapToResponseTeam(team);

    }  
    
    /// <summary>
    /// Retorna todos os times do usuario
    /// </summary>
    public async Task<List<TeamResponseDto>> GetAllAsync(int userId)
    {
        var teams = await _teamRepository.GetAllByUserAsync(userId);

       return teams.Select(MapToResponseTeam).ToList();
    }
    
    /// <summary>
    /// Retorna um time do usuario
    /// </summary>
    public async Task<TeamResponseDto> GetByIdAsync(int userId, int teamid)
    {
        var team = await _teamRepository.GetByIdAsync(userId, teamid);
        
        if (team == null)
            throw new InvalidOperationException("Time não existe");

        return MapToResponseTeam(team);
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

        return player.Select(MapToResponsePlayer).ToList();
    }
    
    /// <summary>
    /// Retorna todas as partidas de um time do usuario
    /// </summary>
    public async Task<List<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId)
    {
        var teamExists = await _teamRepository.GetByIdAsync(userId, teamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var match = await _matchRepository.GetAllMatchesByTeamAsync(userId, teamId);

        return match.Select(MapToResponseMatch).ToList();
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

            return MapToResponseTeam(team);
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




    /// <summary>
    /// Metodo que retorna os dados salvos do Time
    /// </summary>
    private static TeamResponseDto MapToResponseTeam(Team team)
    {
        return new TeamResponseDto
        {
            Id = team.Id,
            Name = team.Name
        };
    }

    /// <summary>
    /// Metodo que retorna os dados salvos do player
    /// </summary>
    private static PlayerResponseDto MapToResponsePlayer(Player player)
    {
        return new PlayerResponseDto
        {
            Id = player.Id,
            Name = player.Name,
            Position = player.Position,
            ShirtNumber = player.ShirtNumber,
            Goals = player.Goals,
            Assists = player.Assists,
            MatchesPlayed = player.MatchesPlayed
        };
    }

    /// <summary>
    /// Metodo que retorna os dados salvos da partida
    /// </summary>
    private static MatchResponseDto MapToResponseMatch(Match match)
    {
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
}