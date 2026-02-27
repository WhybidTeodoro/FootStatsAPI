using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
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
        var pagination = new PaginationParameters();
        var sorting = new SortParameters();

        var paged = await GetAllAsync(userId, pagination, sorting);

        return paged.Items.ToList();
    }

    /// <summary>
    /// Retorna todos os jogadores de um time do usuario (com paginação e ordenação).
    /// </summary>
    public async Task<PagedResult<TeamResponseDto>> GetAllAsync(int userId, PaginationParameters pagination, SortParameters sorting)
    {
        NormalizePagination(pagination);

        var teams = await _teamRepository.GetAllByUserAsync(userId);

        var dtos = teams.Select(MapToResponseTeam).ToList();

        var ordered = ApplyTeamSorting(dtos, sorting);

        return ToPagedResult(ordered, pagination);
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
    /// Retorna todos os jogadores de um time do usuario (com paginação e ordenação).
    /// </summary>
    public async Task<PagedResult<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
    {
        NormalizePagination(pagination);

       
        var team = await _teamRepository.GetByIdAsync(userId, teamId);
        if (team == null)
            throw new InvalidOperationException("Time não encontrado");

        
        var players = await _playerRepository.GetAllByTeamAsync(userId, teamId);

        var dtos = players.Select(MapToResponsePlayer).ToList();

        var ordered = ApplyPlayerSorting(dtos, sorting);

        return ToPagedResult(ordered, pagination);
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
    /// Retorna todas as partidas de um time do usuario (com paginação e ordenação).
    /// </summary>
    public async Task<PagedResult<MatchResponseDto>> GetAllMatchesByTeamAsync(int userId, int teamId, PaginationParameters pagination, SortParameters sorting)
    {
        NormalizePagination(pagination);

        var teamExists = await _teamRepository.GetByIdAsync(userId, teamId);
        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var matches = await _matchRepository.GetAllMatchesByTeamAsync(userId, teamId);

        var dtos = matches.Select(MapToResponseMatch).ToList();

        var ordered = ApplyMatchSorting(dtos, sorting);

        return ToPagedResult(ordered, pagination);
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
    /// Garante que PageNumber e PageSize fiquem dentro de limites seguros.
    /// </summary>
    private static void NormalizePagination(PaginationParameters pagination)
    {
        if (pagination.PageNumber < PaginationParameters.MinPageNumber)
            pagination.PageNumber = PaginationParameters.DefaultPageNumber;

        if (pagination.PageSize < PaginationParameters.MinPageSize)
            pagination.PageSize = PaginationParameters.DefaultPageSize;

        if (pagination.PageSize > PaginationParameters.MaxPageSize)
            pagination.PageSize = PaginationParameters.MaxPageSize;

    }


    /// <summary>
    /// Normaliza o SortBy para comparar de forma consistente.
    /// </summary>
    private static string NormalizeSortBy(string? sortBy)
    {
        if (string.IsNullOrEmpty(sortBy))
            return "id";

        return sortBy.Trim().ToLowerInvariant();
    }


    /// <summary>
    /// Aplica direção Asc/Desc em uma sequência já ordenada.
    /// </summary>
    private static List<T> ApplyDirection<T>(IOrderedEnumerable<T> ordered, SortDirection direction)
    {
        return direction == SortDirection.Desc
            ? ordered.Reverse().ToList()
            : ordered.ToList();
    }

    /// <summary>
    /// Aplica ordenação segura para TeamResponseDto.
    /// </summary>
    private static List<TeamResponseDto> ApplyTeamSorting(List<TeamResponseDto> items, SortParameters sorting)
    {
        var sortBy = NormalizeSortBy(sorting.SortBy);

        return sortBy switch
        {
        "name" => ApplyDirection(items.OrderBy(x => x.Name), sorting.Direction), _ => ApplyDirection(items.OrderBy(x => x.Id), sorting.Direction)
        };
    }

    /// <summary>
    /// Aplica ordenação segura para PlayerResponseDto.
    /// </summary>
    private static List<PlayerResponseDto> ApplyPlayerSorting(List<PlayerResponseDto> items, SortParameters sorting)
    {
        var sortBy = NormalizeSortBy(sorting.SortBy);

        return sortBy switch
        {
            "name" => ApplyDirection(items.OrderBy(x => x.Name), sorting.Direction),
            "position" => ApplyDirection(items.OrderBy(x => x.Position), sorting.Direction),
            "goals" => ApplyDirection(items.OrderBy(x => x.Goals), sorting.Direction),
            "assists" => ApplyDirection(items.OrderBy(x => x.Assists), sorting.Direction),
            "matchesplayed" => ApplyDirection(items.OrderBy(x => x.MatchesPlayed), sorting.Direction),
            "shirtnumber" => ApplyDirection(items.OrderBy(x => x.ShirtNumber), sorting.Direction),
            _ => ApplyDirection(items.OrderBy(x => x.Id), sorting.Direction)
        };
    }


    /// <summary>
    /// Aplica ordenação segura para MatchResponseDto.
    /// </summary>
    private static List<MatchResponseDto> ApplyMatchSorting(List<MatchResponseDto> items, SortParameters sorting)
    {
        var sortBy = NormalizeSortBy(sorting.SortBy);

        return sortBy switch
        {
            "matchdate" => ApplyDirection(items.OrderBy(x => x.MatchDate), sorting.Direction),
            "opponentteam" => ApplyDirection(items.OrderBy(x => x.OpponentTeam), sorting.Direction),
            "goalsfor" => ApplyDirection(items.OrderBy(x => x.GoalsFor), sorting.Direction),
            "goalsagainst" => ApplyDirection(items.OrderBy(x => x.GoalsAgainst), sorting.Direction),
            _ => ApplyDirection(items.OrderBy(x => x.Id), sorting.Direction)
        };
    }

    /// <summary>
    /// Converte uma lista (já ordenada) em PagedResult usando Skip/Take.
    /// </summary>
    private static PagedResult<T> ToPagedResult<T>(List<T> orderedItems, PaginationParameters pagination)
    {
        var totalCount = orderedItems.Count;

        var skip = pagination.GetSkipCount();

        var pageItems = orderedItems
            .Skip(skip)
            .Take(pagination.PageSize)
            .ToList();

        return PagedResult<T>.From(
            items: pageItems,
            pageNumber: pagination.PageNumber,
            pageSize: pagination.PageSize,
            totalCount: totalCount);
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