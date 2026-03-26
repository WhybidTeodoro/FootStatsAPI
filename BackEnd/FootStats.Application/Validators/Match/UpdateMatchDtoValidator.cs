using FluentValidation;
using FootStatsAPI.DTOs.Match;

namespace FootStats.Application.Validators.Match;

/// <summary>
/// Responsavel pela validação dos dados na atualização de uma partida 
/// </summary>
public class UpdateMatchDtoValidator : AbstractValidator<UpdateMatchDto>
{
    public UpdateMatchDtoValidator()
    {
        RuleFor(m => m.MatchDate)
            .NotEmpty().WithMessage("A data da partida é obrigatória");

        RuleFor(m => m.OpponentTeam)
            .NotEmpty().WithMessage("O nome do time adversário é obrigatório")
            .MaximumLength(50).WithMessage("O Nome do time pode conter no máximo 50 caracteres"); ;

        RuleFor(m => m.GoalsFor)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de gols inválida");

        RuleFor(m => m.GoalsAgainst)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de gols inválida");

    }
}
