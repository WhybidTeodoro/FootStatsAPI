using FootStats.API.Contracts.Query;
using FootStats.API.Controllers;
using FootStats.Application.Common.Pagination;
using FootStats.Application.Common.Sorting;
using FootStatsAPI.DTOs.Team;
using FootStatsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel pelos times do usuario
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController : BaseController
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
        try
        {
            var userId = GetUserId();
            var result = await _teamService.AddTeamAsync(userId, dto);
            return Created(string.Empty, result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
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
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationQueryParameters paginationQuery,
        [FromQuery] SortQueryParameters sortQuery) 
    {
        try
        {
            var userId = GetUserId();

            var (pageNumber, pageSize) = paginationQuery.ToNormalized();

            var pagination = new PaginationParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var (sortBy, sortDirectionText) = sortQuery.ToNormalized();

            var sorting = new SortParameters
            {
                SortBy = sortBy,
                Direction = SortParametersParser.ParseOrDefault(sortDirectionText)
            };

            var teams = await _teamService.GetAllAsync(userId, pagination, sorting);

            return Ok(teams);

        }
        catch (UnauthorizedAccessException ex)
        {

            return Unauthorized(new { message = ex.Message });
        }
        
        
    }

    /// <summary>
    /// Endpoint responsavel por buscar o time do usuario pelo id do time
    /// </summary>
    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetById(int teamId)
    {
        try
        {
            var userId = GetUserId();

            var team = await _teamService.GetByIdAsync(userId, teamId);

            return Ok(team);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    
    }

    /// <summary>
    /// Endpoint responsavel por retornar todos os jogadores de um time
    /// </summary>
    [HttpGet("{teamId}/players")]
    public async Task<IActionResult> GetAllPlayersByTeam(
        int teamId,
        [FromQuery] PaginationQueryParameters paginationQuery,
        [FromQuery] SortQueryParameters sortQuery
        )
    {
        try
        {
            var userId = GetUserId();

            var (pageNumber, pageSize) = paginationQuery.ToNormalized();

            var pagination = new PaginationParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            var (sortBy, sortDirectionText) = sortQuery.ToNormalized();

            var sorting = new SortParameters
            {
                SortBy = sortBy,
                Direction = SortParametersParser.ParseOrDefault(sortDirectionText)
            };

            var players = await _teamService.GetAllPlayersByTeamAsync(userId, teamId, pagination, sorting);

            return Ok(players);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
                                       
    }

    /// <summary>
    /// Endpoint responsavel por retornar todas as partidas de um time do usuario
    /// </summary>
    [HttpGet("{teamId}/matches")]
    public async Task<IActionResult> GetAllMatchesByTeam(
        int teamId,
        [FromQuery] PaginationQueryParameters paginationQuery,
        [FromQuery] SortQueryParameters sortQuery)
    {
        try
        {
            var userId = GetUserId();

            var (pageNumber, pageSize) = paginationQuery.ToNormalized();

            var pagination = new PaginationParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            var (sortBy, sortDirectionText) = sortQuery.ToNormalized();

            var sorting = new SortParameters
            {
                SortBy = sortBy,
                Direction = SortParametersParser.ParseOrDefault(sortDirectionText)
            };

            var matches = await _teamService.GetAllMatchesByTeamAsync(userId, teamId, pagination, sorting);

            return Ok(matches);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Endpoint responsavel por atualizar um time do usuario
    /// </summary>
    [HttpPut("{teamId}")]
    public async Task<IActionResult> UpdateTeam(int teamId, UpdateTeamDto dto)
    {
        try
        {
            var userId = GetUserId();

            var team = await _teamService.UpdateTeamAsync(userId, teamId, dto);

            return Ok(team);
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Endpoint responsavel por deletar um time do usuario.
    /// </summary>
    [HttpDelete("{teamId}")]
    public async Task<IActionResult> DeleteTeam(int teamId)
    {
        try
        {
            var userId = GetUserId();

            await _teamService.DeleteTeamAsync(userId, teamId);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {

            return NotFound(new { message = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
