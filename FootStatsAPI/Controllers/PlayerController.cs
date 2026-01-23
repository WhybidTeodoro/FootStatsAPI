using FootStatsAPI.DTOs.Player;
using FootStatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel por gerenciar os jogadores
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlayerController : ControllerBase
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
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var player = await _playerService.AddPlayerAsync(userId, dto);

            return Created(string.Empty, player);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var player = await _playerService.GetByIdAsync(userId, id);

            return Ok(player);

        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
        
    }

    /// <summary>
    /// Endpoint responsavel por atualizar os dados de perfil do jogador
    /// </summary>
    [HttpPut("{id}/profile")]
    public async Task<IActionResult> UpdatePLayerProfile(int id, UpdatePlayerProfileDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var player = await _playerService.UpdatePlayerProfileAsync(id, userId, dto);
    
            return Ok(player);

        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
        
   
    }

    /// <summary>
    /// Endpoint responsavel por atualizar as estatisticas do jogador
    /// </summary>
    [HttpPut("{id}/stats")]
    public async Task<IActionResult> UpdatePlayerStats(int id, UpdatePlayerStatsDto dto)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            var player = await _playerService.UpdatePlayerStatsAsync(id, userId, dto);

            return Ok(player);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint responsavel por deletar um jogador da base de dados
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized(new { message = "Token invalido ou usuario não autenticado" });

        try
        {
            await _playerService.DeletePlayerAsync(id, userId);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);

        }
    }
}
