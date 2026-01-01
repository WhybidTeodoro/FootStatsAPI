using FootStatsAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootStatsAPI.Controllers;

/// <summary>
/// Controller responsavel por calcular a estatisticas dos times
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatsController : ControllerBase
{
    private readonly FootDbContext _context;

    public StatsController(FootDbContext context)
    {
        _context = context;
    }
}
