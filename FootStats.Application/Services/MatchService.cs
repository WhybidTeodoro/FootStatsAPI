using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;

namespace FootStatsAPI.Services;

/// <summary>
/// Service que implementa a regra de negocio para a entidade Match
/// </summary>
public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITeamRepository _teamRepository;

    public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
    }

    /// <summary>
    /// Adiciona uma partida a um time do usuario
    /// </summary>
    public async Task<MatchResponseDto> AddMatchAsync(int userId, CreateMatchDto dto)
    {
        var teamExists = await _teamRepository.GetByIdAsync(userId, dto.TeamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

        var match = new Match
        {
            OpponentTeam = dto.OpponentTeam,
            MatchDate = dto.MatchDate,
            GoalsFor = dto.GoalsFor,
            GoalsAgainst = dto.GoalsAgainst,
            TeamId = dto.TeamId,
            CreatedAt = DateTime.UtcNow
        };

        await _matchRepository.AddAsync(match);
        await _matchRepository.SaveChangesAsync();

        return MapToResponse(match);
    }

    /// <summary>
    /// Retorna uma partida de um time do usuario
    /// </summary>
    public async Task<MatchResponseDto> GetByIdAsync(int userId, int id)
    {
        var match = await _matchRepository.GetByIdAsync(userId, id);

        if (match == null)
            throw new InvalidOperationException("Partida não encontrada");

        return MapToResponse(match);
    }

    /// <summary>
    /// Atualiza uma partida de um time do usuario
    /// </summary>
    public async Task<MatchResponseDto> UpdateMatchAsync(int userId, int id, UpdateMatchDto dto)
    {
        var match = await _matchRepository.GetByIdAsync(userId, id);

        if (match == null)
            throw new InvalidOperationException("Partida não encontrada");

        match.MatchDate = dto.MatchDate;
        match.OpponentTeam = dto.OpponentTeam;
        match.GoalsFor = dto.GoalsFor;
        match.GoalsAgainst = dto.GoalsAgainst;
        match.UpdatedAt = DateTime.UtcNow;

        await _matchRepository.UpdateAsync(match);
        await _matchRepository.SaveChangesAsync();

        return MapToResponse(match);
    }

    /// <summary>
    /// Deleta uuma partida de um time do usuario
    /// </summary>
    public async Task DeleteMatchAsync(int userId, int id)
    {
        var match = await _matchRepository.GetByIdAsync(userId, id);

        if (match == null)
            throw new InvalidOperationException("Partida não encontrada");

        await _matchRepository.DeleteAsync(match);
        await _matchRepository.SaveChangesAsync();
    }


    /// <summary>
    /// Metodo que retorna os dados salvos da partida
    /// </summary>
    private static MatchResponseDto MapToResponse(Match match)
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
