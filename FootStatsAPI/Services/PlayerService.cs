using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootStatsAPI.Services;

public class PlayerService : IPlayerService
{

    private readonly FootDbContext _context;

    public PlayerService(FootDbContext context)
    {
        _context = context;
    }

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
            Name = player.Name,
            Position = player.Position,
            ShirtNumber = player.ShirtNumber,
            Goals = player.Goals,
            Assists = player.Assists,
            MatchesPlayed = player.MatchesPlayed
        };
    }

    public async Task<PlayerResponseDto> GetByIdAsync(int id, int userId)
    {
        var player = await _context.Players.Where(p =>  p.Id == id && p.Team.UserId == userId)
            .Select(p => new PlayerResponseDto
            {
                Name = p.Name,
                Position = p.Position,
                ShirtNumber = p.ShirtNumber,
                Goals= p.Goals,
                Assists = p.Assists,
                MatchesPlayed= p.MatchesPlayed
            }).FirstOrDefaultAsync();

        if (player == null)
            throw new InvalidOperationException("Jogador não registrado");

        return (player);


    }

    public Task<PlayerResponseDto> UpdatePlayerProfileAsync(int id, int userId, UpdatePlayerProfileDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<PlayerResponseDto> UpdatePlayerStatsAsync(int id, int userId, UpdatePlayerStatsDto dto)
    {
        throw new NotImplementedException();
    }
    public Task DeletePlayerAsync(int id, int userId)
    {
        throw new NotImplementedException();
    }
}
