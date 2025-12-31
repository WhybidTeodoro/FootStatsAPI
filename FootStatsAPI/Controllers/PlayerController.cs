using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel por gerenciar os jogadores
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly FootDbContext _context;

    public PlayerController(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// endpoint responsavel por adicionar novos jogadores
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddPlayer(CreatePlayerDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == dto.TeamId && t.UserId == userId);

        if (team == null)
            return NotFound(new { message = "O Time não existe" });


        try
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

            var playerAlreadyExists = await _context.Players
                .AnyAsync(p => p.Name == player.Name 
                && p.Position == player.Position
                && p.ShirtNumber == player.ShirtNumber 
                && p.TeamId == player.TeamId);

            if (playerAlreadyExists)
                return BadRequest(new { message = "Jogador já cadastrado neste time" });

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            var response = new PlayerResponseDto
            {
                Id = player.Id,
                Name = player.Name,
                Position = player.Position,
                ShirtNumber = player.ShirtNumber,
                Goals = player.Goals,
                Assists = player.Assists,
                MatchesPlayed = player.MatchesPlayed
            };

            return Created(string.Empty, response);

        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar criar o jogador" });
        }
    }

}
