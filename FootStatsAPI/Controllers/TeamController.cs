using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Models;
using FootStatsAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

    private readonly TeamService _teamService;


    public TeamController(FootDbContext context, TeamService teamService)
    {
        _context = context;
        _teamService = teamService;
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
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var result = await _teamService.AddTeamAsync(userId, dto);
            return Created(string.Empty, result);
        }
        catch(InvalidOperationException ex)
        {
            return NotFound(new {message = ex.Message});
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        var result = await _teamService.GetByIdAsync(userId, id);

        if (result == null)
            return NotFound(new { message = "Time não encontrado" });
         
        return Ok(result);
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
        catch (InvalidOperationException)
        {

            return NotFound(new { message = "Time não encontrado" });
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
            var matches = await _teamService.GetAllMatchessByTeamAsync(userId, teamId);

            return Ok(matches);
        }
        catch (InvalidOperationException)
        {

            return NotFound(new { message = "Time não encontrado" });
        }
    }

    /// <summary>
    /// Endpoint responsavel por atualizar um time do usuario
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(int id, UpdateTeamDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

     
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (team == null)
                return NotFound(new { message = "Time não encontrado" });

            team.Name = dto.Name;

            await _context.SaveChangesAsync();

            var response = new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name
            };

            return Ok(response);
    }

    /// <summary>
    /// Endpoint responsavel por deletar um time do usuario.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (team == null)
            return NotFound(new { message = "Time não encontrado" });

        try
        { 
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar excluir o time" });
        }
    }
}
