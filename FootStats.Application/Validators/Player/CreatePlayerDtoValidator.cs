using FluentValidation;
using FootStatsAPI.DTOs.Player;

namespace FootStats.Application.Validators.Player;

public class CreatePlayerDtoValidator : AbstractValidator<CreatePlayerDto>
{
    public CreatePlayerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100);

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Posição obrigatória");

        RuleFor(x => x.ShirtNumber)
            .InclusiveBetween(0, 99)
            .WithMessage("O Numero da camisa deve ser entre 0 e 99");
    }
}
