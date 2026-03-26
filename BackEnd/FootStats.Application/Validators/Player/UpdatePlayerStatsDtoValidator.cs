using FluentValidation;
using FootStatsAPI.DTOs.Player;

namespace FootStats.Application.Validators.Player;

/// <summary>
/// Responsavel pela validação dos dados na atualização das estatisticas do jogador
/// </summary>
public class UpdatePlayerStatsDtoValidator : AbstractValidator<UpdatePlayerStatsDto>
{
    public UpdatePlayerStatsDtoValidator()
    {
        RuleFor(x => x.Goals)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O Numero de gols é no minimo 0");

        RuleFor(x => x.Assists)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O Numero de assistências é no minimo 0");

        RuleFor(x => x.MatchesPlayed)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O Numero de partidas disputadas é no minimo 0");

    }
}
