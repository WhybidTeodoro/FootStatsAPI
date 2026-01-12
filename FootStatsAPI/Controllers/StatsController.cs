using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Stats;
using FootStatsAPI.Services.Interfaces;
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
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet("/Team/{teamId}/stats")]
    public async Task<IActionResult> GetAllStatsByTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if(userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        try
        {
            var stats = await _statsService.GetAllStatsByTeam(userId, teamId);

            return Ok(stats);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
