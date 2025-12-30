using FootStatsAPI.Data;
using FootStatsAPI.DTOs.User;
using FootStatsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FootStatsAPI.Controllers;
/// <summary>
/// Controller Utilizado para registro e login/Authenticação do usuario
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly FootDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(FootDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Endtpoint utilizado para registrar usuario.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        bool emailExists = await _context.Users.AnyAsync(e => e.Email == dto.Email);
        if (emailExists)
            return BadRequest(new { message = "Email já cadastro" });

        try
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created(string.Empty, new { user.Id, user.Name, user.Email });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro interno ao registrar usuario" });
        }
    }

    /// <summary>
    /// Endpoint utilizado para login do usuario
    /// </summary>
    [HttpPost("login")] 
    public async Task<IActionResult> LoginUser(LoginUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == dto.Email);

            if (user == null)
                return Unauthorized(new { message = "Email ou senha invalidos" });

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Email ou senha invalidos" });

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                };

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(jwtSettings["ExpiresInMinutes"]!)),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new {token = tokenString, expiresIn = jwtSettings["ExpiresInMinutes"] });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro interno ao realizar o login do usuario" });
        }
    }



}
