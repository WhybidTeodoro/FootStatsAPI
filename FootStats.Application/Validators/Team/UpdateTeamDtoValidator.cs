using FluentValidation;
using FootStatsAPI.DTOs.Team;

namespace FootStats.Application.Validators.Team;

public class UpdateTeamDtoValidator : AbstractValidator<UpdateTeamDto>
{
	public UpdateTeamDtoValidator()
	{
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("O Nome do time é obrigatório")
            .MaximumLength(50).WithMessage("O Nome do time deve ter no máximo 50 caracteres");
    }
}
