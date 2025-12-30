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
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

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

        try
        {
            var teams = await _context.Teams.Where(team => team.UserId == userId)
                .Select(team => new TeamResponseDto
                {
                    Id = team.Id,
                    Name = team.Name
                }).ToListAsync();

            return Ok(teams);
        }
        catch (Exception)
        {

            return StatusCode(500, new { message = "Erro interno ao tentar listar os times do usuario" });
        }
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

        var response = await _context.Teams.Where(team => team.Id == id && team.UserId == userId)
                    .Select(team => new TeamResponseDto
                    {
                        Id = team.Id,
                        Name = team.Name
                    }).FirstOrDefaultAsync();

        if (response == null)
            return NotFound(new { message = "Time não encontrado" });
         

        return Ok(response);
     
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(int id, UpdateTeamDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        try
        {
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
        catch (Exception)
        {

            throw;
        }
       

    }
}
