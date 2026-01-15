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

    /// <summary>
    /// Retorna as estatisticas das partidas do usuario
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    [HttpGet("team/{teamId}/stats")]
    public async Task<IActionResult> GetStatsByTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if(userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        try
        {
            var stats = await _statsService.GetStatsByTeam(userId, teamId);

            return Ok(stats);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
