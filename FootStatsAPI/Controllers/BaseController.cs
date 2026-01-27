using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FootStats.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            throw new UnauthorizedAccessException("Token invalido ou usuario não autenticado");

        return userId;
    }
}
