using System.ComponentModel.DataAnnotations;

namespace FootStatsAPI.DTOs.User;

/// <summary>
/// Dto utilizado para registro do usuario
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    /// Nome do usuario.
    /// </summary>
    [Required(ErrorMessage = "O Nome do usuario é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no maximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuario. Usado no registro do usuario em sistema.
    /// </summary>
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email informado é invalido")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Representa a senha do usuario. Usado no registro do usuario em sistema.
    /// </summary>
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
    public string Password { get; set; } = string.Empty;
}
