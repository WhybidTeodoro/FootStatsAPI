using FluentValidation;
using FootStatsAPI.DTOs.Team;

namespace FootStats.Application.Validators.Team;

/// <summary>
/// Responsavel pela validação dos dados na criação do time
/// </summary>
public class CreateTeamDtoValidator : AbstractValidator<CreateTeamDto>
{
    public CreateTeamDtoValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("O Nome do time é obrigatório")
            .MaximumLength(50).WithMessage("O Nome do time deve ter no máximo 50 caracteres");
    }
}
