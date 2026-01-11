using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;


/// <summary>
/// Service que implementa a regra de negocio para a entidade Player
/// </summary>
public class PlayerService : IPlayerService
{

    private readonly FootDbContext _context;

    public PlayerService(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona um jogador a um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> AddPlayerAsync(int userId, CreatePlayerDto dto)
    {
        var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == dto.TeamId);

        if (teamExists == null)
            throw new InvalidOperationException("Time não encontrado");

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

        var playerExists = await _context.Players.AnyAsync(p =>
        p.Name == player.Name &&
        p.Position == player.Position &&
        p.ShirtNumber == player.ShirtNumber &&
        p.TeamId == dto.TeamId);

        if (playerExists)
            throw new InvalidOperationException("Jogador já registrado");

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

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
        var player = await _context.Players.Where(p =>  p.Id == id && p.Team.UserId == userId)
            .Select(p => new PlayerResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Position = p.Position,
                ShirtNumber = p.ShirtNumber,
                Goals= p.Goals,
                Assists = p.Assists,
                MatchesPlayed= p.MatchesPlayed
            }).FirstOrDefaultAsync();

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        return player;


    }

    /// <summary>
    /// Atualiza o perfil de um jogador registrado em um time do usuario
    /// </summary>
    public async Task<PlayerResponseDto> UpdatePlayerProfileAsync(int userId, int id, UpdatePlayerProfileDto dto)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id && p.Team.UserId == userId);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");


        player.Name = dto.Name;
        player.Position = dto.Position;
        player.ShirtNumber = dto.ShirtNumber;
        
        await _context.SaveChangesAsync();

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
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id && p.Team.UserId == userId);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        player.Goals = dto.Goals;
        player.Assists = dto.Assists;
        player.MatchesPlayed = dto.MatchesPlayed;

        await _context.SaveChangesAsync();

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
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id && p.Team.UserId == userId);

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }
}
