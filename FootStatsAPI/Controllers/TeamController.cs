using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private readonly ITeamService _teamService;


    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// Endpoint Responsavel por criar um time
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddTeam(CreateTeamDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var result = await _teamService.AddTeamAsync(userId, dto);
            return Created(string.Empty, result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar criar o time para o usuario" });
        } 
    }

    /// <summary>
    /// Endpoint responsavel por retornar todos os times do usuario
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        var teams = await _teamService.GetAllAsync(userId);

        return Ok(teams);
        
    }

    /// <summary>
    /// Endpoint responsavel por buscar o time do usuario pelo id do time
    /// </summary>
    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetById(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var team = await _teamService.GetByIdAsync(userId, teamId);

            return Ok(team);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
    
    }

    /// <summary>
    /// Endpoint responsavel por retornar todos os jogadores de um time
    /// </summary>
    [HttpGet("{teamId}/players")]
    public async Task<IActionResult> GetAllPlayersByTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var players = await _teamService.GetAllPlayersByTeamAsync(userId, teamId);
            return Ok(players);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
                                       
    }

    /// <summary>
    /// Endpoint responsavel por retornar todas as partidas de um time do usuario
    /// </summary>
    [HttpGet("{teamId}/matches")]
    public async Task<IActionResult> GetAllMatchesByTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();
        try
        {
            var matches = await _teamService.GetAllMatchesByTeamAsync(userId, teamId);

            return Ok(matches);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint responsavel por atualizar um time do usuario
    /// </summary>
    [HttpPut("{teamId}")]
    public async Task<IActionResult> UpdateTeam(int teamId, UpdateTeamDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var team = await _teamService.UpdateTeamAsync(userId, teamId, dto);

            return Ok(team);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint responsavel por deletar um time do usuario.
    /// </summary>
    [HttpDelete("{teamId}")]
    public async Task<IActionResult> DeleteTeam(int teamId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });
        try
        {
             await _teamService.DeleteTeamAsync(userId, teamId);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
    }
}
