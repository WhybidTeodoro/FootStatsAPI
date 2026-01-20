using FootStats.Application.Services.Interfaces.Repositories;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;

namespace FootStatsAPI.Services;


/// <summary>
/// Service que implementa a regra de negocio para a entidade Player
/// </summary>
public class PlayerService : IPlayerService
{

    private readonly IPlayerRepository _playerRepository;
    private readonly ITeamRepository _teamRepository;

    public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
    }

    /// <summary>
    /// Adiciona um jogador a um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> AddPlayerAsync(int userId, CreatePlayerDto dto)
    {
        var player = new Player
        {
            Name = dto.Name,
            Position = dto.Position,
            ShirtNumber = dto.ShirtNumber,
            Goals = dto.Goals,
            Assists = dto.Assists,
            MatchesPlayed = dto.MatchesPlayed,
            TeamId = dto.TeamId,
            CreatedAt = DateTime.UtcNow
        };

        var teamExists = await _teamRepository.GetByIdAsync(userId, player.TeamId);

        if (teamExists == null)
           throw new InvalidOperationException("Time não encontrado");

        var playerExists = await _playerRepository.ExistsAsync(dto.Name, dto.Position, dto.ShirtNumber, dto.TeamId);

        if (playerExists)
            throw new InvalidOperationException("Jogador já registrado");

        await _playerRepository.AddAsync(player);
        await _playerRepository.SaveChangesAsync();

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
    /// Retorna um jogador de um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> GetByIdAsync(int userId, int id)
    {
        var player = await _playerRepository.GetByIdAsync(userId, id);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

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
    /// Atualiza o perfil de um jogador registrado em um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> UpdatePlayerProfileAsync(int userId, int id, UpdatePlayerProfileDto dto)
    {
        var player = await _playerRepository.GetByIdAsync(userId, id);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        player.Name = dto.Name;
        player.Position = dto.Position;
        player.ShirtNumber = dto.ShirtNumber;
        player.UpdatedAt = DateTime.UtcNow;
        
        await _playerRepository.UpdateAsync(player);
        await _playerRepository.SaveChangesAsync();

        return new PlayerResponseDto
        {
            Id = id,
            Name = player.Name,
            Position = player.Position,
            ShirtNumber = player.ShirtNumber,
            Goals = player.Goals,
            Assists = player.Assists,
            MatchesPlayed = player.MatchesPlayed
        };
    }

    /// <summary>
    /// Atualiza as estatisticas de um jogador registrado em um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> UpdatePlayerStatsAsync(int userId, int id, UpdatePlayerStatsDto dto)
    {
        var player = await _playerRepository.GetByIdAsync(userId, id);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        player.Goals = dto.Goals;
        player.Assists = dto.Assists;
        player.MatchesPlayed = dto.MatchesPlayed;

        await _playerRepository.UpdateAsync(player);
        await _playerRepository.SaveChangesAsync();

        return new PlayerResponseDto
        {
            Id = id,
            Name = player.Name,
            Position = player.Position,
            ShirtNumber = player.ShirtNumber,
            Goals = player.Goals,
            Assists = player.Assists,
            MatchesPlayed = player.MatchesPlayed
        };
    }

    /// <summary>
    /// Deleta um jogador registrado de um time do usuario 
    /// </summary>
    public async Task DeletePlayerAsync(int userId, int id)
    {
        var player = await _playerRepository.GetByIdAsync(userId, id);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        await _playerRepository.DeleteAsync(player);
        await _playerRepository.SaveChangesAsync();
    }
}
