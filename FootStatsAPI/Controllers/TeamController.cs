using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel pelos times do usuario
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly FootDbContext _context;

    public TeamController(FootDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Endpoint Responsavel por criar um time
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddTeam(CreateTeamDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized(new { message = "Token invalido ou sem identifação do usuario" });

        var teamAlreadyExists = await _context.Teams.AnyAsync(t => t.UserId == userId && t.Name == dto.Name);

        if (teamAlreadyExists)
            return BadRequest(new {message = "Você ja possui um time com esse nome"});

        try
        {
            var team = new Team
            {
                Name = dto.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            var response = new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name
            };

            return Created(string.Empty, response);
        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar criar o time para o usuario" });
        }

        
    }


}
