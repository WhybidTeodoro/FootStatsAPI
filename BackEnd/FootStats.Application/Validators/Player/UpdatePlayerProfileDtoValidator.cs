using FluentValidation;
using FootStatsAPI.DTOs.Player;

namespace FootStats.Application.Validators.Player;

/// <summary>
/// Responsavel pela validação dos dados na atualização do perfil do jogador
/// </summary>
public class UpdatePlayerProfileDtoValidator : AbstractValidator<UpdatePlayerProfileDto>
{
    public UpdatePlayerProfileDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Posição obrigatória");

        RuleFor(x => x.ShirtNumber)
            .InclusiveBetween(0, 99)
            .WithMessage("O Numero da camisa deve ser entre 0 e 99");
    }
}
