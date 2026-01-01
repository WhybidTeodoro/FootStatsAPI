using FootStatsAPI.Data;
using FootStatsAPI.DTOs.Match;
using FootStatsAPI.Models;
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

    public MatchController(FootDbContext context)
    {
        _context = context;
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

        var teamExist = await _context.Teams.FirstOrDefaultAsync(t => t.Id == dto.TeamId && t.UserId == userId);

        if (teamExist == null)
            return NotFound(new { message = "O Time não existe" });

        try
        {
            var match = new Match
            {
                MatchDate = dto.MatchDate,
                OpponentTeam = dto.OpponentTeam,
                GoalsFor = dto.GoalsFor,
                GoalsAgainst = dto.GoalsAgainst,
                TeamId = dto.TeamId
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            var response = new MatchResponseDto
            {
                Id = match.Id,
                MatchDate = match.MatchDate,
                OpponentTeam = match.OpponentTeam,
                GoalsFor = match.GoalsFor,
                GoalsAgainst = match.GoalsAgainst,
                TeamId = match.TeamId
            };

            return Created(string.Empty, response);
        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar criar o jogador" });
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

        var match = await _context.Matches.Where(m => m.Id == id && m.Team.UserId == userId)
            .Select(match => new MatchResponseDto
            {
                Id = match.Id,
                MatchDate = match.MatchDate,
                OpponentTeam = match.OpponentTeam,
                GoalsFor = match.GoalsFor,
                GoalsAgainst = match.GoalsAgainst,
                TeamId = match.TeamId
            }).FirstOrDefaultAsync();

        if (match == null)
            return NotFound(new { message = "Partida não encontrada" });

        return Ok(match);
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

        var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id && m.Team.UserId == userId);

        if (match == null) 
            return NotFound(new { message = "Partida não encontrada" });

        match.MatchDate = dto.MatchDate;
        match.OpponentTeam = dto.OpponentTeam;
        match.GoalsFor = dto.GoalsFor;
        match.GoalsAgainst = dto.GoalsAgainst;

        await _context.SaveChangesAsync();

        var response = new MatchResponseDto
        {
            MatchDate = match.MatchDate,
            OpponentTeam = match.OpponentTeam,
            GoalsFor = dto.GoalsFor,
            GoalsAgainst = dto.GoalsAgainst,
            TeamId = match.TeamId
            
        };

        return Ok(response);
    }

    /// <summary>
    /// Endpoint responsavel por deletar uma partida especifica de um time do usuario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id && m.Team.UserId == userId);

        if (match == null) 
            return NotFound(new { message = "Partida não encontrada" });

        try
        {
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar excluir a partida" });
        }
        
    }
}
