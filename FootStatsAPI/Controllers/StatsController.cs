using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Stats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel pelas estatisticas dos times do usuario
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatsController : ControllerBase
{
    private readonly FootDbContext _context;

    public StatsController(FootDbContext context)
    {
        _context = context;
    }

    [HttpGet("/teams{teamId}/stats")]
    public async Task<IActionResult> GetAllStatsByTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if(userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        var TeamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId && t.UserId == userId);

        if(TeamExists == null)
            return NotFound();


        var matches = await _context.Matches.Where(m => m.TeamId == teamId).ToListAsync();

        var stats = new StatsResponseDto
        {
            TotalMatches = matches.Count,
            Wins = matches.Count(m => m.GoalsFor > m.GoalsAgainst),
            Losses = matches.Count(m => m.GoalsFor < m.GoalsAgainst),
            Draws = matches.Count(m => m.GoalsFor == m.GoalsAgainst),
            TotalGoalsFor = matches.Sum(m => m.GoalsFor),
            TotalGoalsAgainst = matches.Sum(m => m.GoalsAgainst)
        };

        //stats.GoalDifference = stats.TotalGoalsFor - stats.TotalGoalsAgainst;

        return Ok(stats);
    }
}
