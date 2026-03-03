using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
    /// Retorna todos os jogadores de um time do usuario
    /// </summary>
    public async Task<PagedResult<TeamResponseDto>> GetAllAsync(int userId, PaginationParameters pagination, SortParameters sorting)
    {
        var teams = await _teamRepository.GetPagedByUserAsync(userId, pagination, sorting);

        var dtoItems = teams.Items.Select(MapToResponseTeam).ToList();

        return PagedResult<TeamResponseDto>.From(items: dtoItems, pageNumber: teams.PageNumber, pageSize: teams.PageSize, totalCount: teams.TotalCount);
    
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
    public async Task<PagedResult<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
    {

        
        var team = await _teamRepository.GetByIdAsync(userId, teamId);
        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        var players = await _playerRepository
            .GetPagedByTeamAsync(userId, teamId, pagination, sorting);
        var dtoItems = players.Items
            .Select(MapToResponsePlayer)
            .ToList();

        return PagedResult<PlayerResponseDto>.From(
            items: dtoItems,
            pageNumber: players.PageNumber,
            pageSize: players.PageSize,
            totalCount: players.TotalCount);
    }

    /// <summary>
    /// Retorna todas as partidas de um time do usuario
    /// </summary>
    public async Task<PagedResult<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
    {
        var team = await _teamRepository.GetByIdAsync(userId, teamId);
        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        var pagedMatches = await _matchRepository
            .GetPagedByTeamAsync(userId, teamId, pagination, sorting);

        var dtoItems = pagedMatches.Items
            .Select(MapToResponseMatch)
            .ToList();

        return PagedResult<MatchResponseDto>.From(
            items: dtoItems,
            pageNumber: pagedMatches.PageNumber,
            pageSize: pagedMatches.PageSize,
            totalCount: pagedMatches.TotalCount);
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