using FootStats.API.Controllers;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel por gerenciar os jogadores
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayerController : BaseController
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    /// <summary>
    /// endpoint responsavel por adicionar novos jogadores
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddPlayer(CreatePlayerDto dto)
    {
        try
        {
            var userId = GetUserId();

            var player = await _playerService.AddPlayerAsync(userId, dto);

            return Created(string.Empty, player);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro interno ao tentar adicionar o jogador" });
        }
    }

    /// <summary>
    /// Endpoint responsavel por retornar o jogador pelo id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var userId = GetUserId();

            var player = await _playerService.GetByIdAsync(userId, id);

            return Ok(player);

        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }

    }

    /// <summary>
    /// Endpoint responsavel por atualizar os dados de perfil do jogador
    /// </summary>
    [HttpPut("{id}/profile")]
    public async Task<IActionResult> UpdatePLayerProfile(int id, int teamId, UpdatePlayerProfileDto dto)
    {
        try
        {
            var userId = GetUserId();

            var player = await _playerService.UpdatePlayerProfileAsync(userId, id, teamId, dto);
    
            return Ok(player);

        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }


    }

    /// <summary>
    /// Endpoint responsavel por atualizar as estatisticas do jogador
    /// </summary>
    [HttpPut("{id}/stats")]
    public async Task<IActionResult> UpdatePlayerStats(int id, UpdatePlayerStatsDto dto)
    {
        try
        {
            var userId = GetUserId();

            var player = await _playerService.UpdatePlayerStatsAsync(userId, id, dto);

            return Ok(player);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Endpoint responsavel por deletar um jogador da base de dados
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        try
        {
            var userId = GetUserId();

            await _playerService.DeletePlayerAsync(userId, id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });

        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
