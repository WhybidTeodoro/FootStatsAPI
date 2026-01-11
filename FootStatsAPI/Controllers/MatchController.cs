using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.Models;
using FootStatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootStatsAPI.Controllers;
/// <summary>
/// Controller responsavel por gerenciar as partidas dos times do usuario
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly FootDbContext _context;
    private readonly IMatchService _matchService;

    public MatchController(FootDbContext context, IMatchService matchService)
    {
        _context = context;
        _matchService = matchService;
    }

    /// <summary>
    /// Endpoint responsavel por adicionar uma partida a um time do usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddMatch(CreateMatchDto dto)
    {

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var match = await _matchService.AddMatchAsync(userId, dto);

            return Created(string.Empty, match);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception)
        {
            return StatusCode(500, new { message = "Erro interno ao tentar adicionar nova partida" });
        }
      
    }

    /// <summary>
    /// Endpoint responsavel po retornar uma partida especifica de um time do usuario
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var match = await _matchService.GetByIdAsync(userId, id);

            return Ok(match);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint responsavel por atualizar uma partida especifica de um time do usuario
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMatch(int id, UpdateMatchDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var match = await _matchService.UpdateMatchAsync(userId, id, dto);

            return Ok(match);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint responsavel por deletar uma partida especifica de um time do usuario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            await _matchService.DeleteMatchAsync(userId, id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        
    }
}
