using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.User;

/// <summary>
/// Dto utilizado para login do usuario
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// Email do usuario.
    /// </summary>
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email informado é invalido")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Senha do usuario.
    /// </summary>
    [Required(ErrorMessage = "A senha é obrigatória")]
    public string Password { get; set; } = string.Empty;
}
