using FluentValidation;
using FootStats.Application.Common.Pagination;

namespace FootStats.Application.Validators.Pagination;

/// <summary>
/// Validação dos parâmetros de paginação.
/// </summary>
public class PaginationParametersValidator
    : AbstractValidator<PaginationParameters>
{
    public PaginationParametersValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(PaginationParameters.MinPageNumber)
            .WithMessage("PageNumber deve ser maior ou igual a 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(PaginationParameters.MinPageSize)
            .WithMessage("PageSize deve ser maior ou igual a 1.")
            .LessThanOrEqualTo(PaginationParameters.MaxPageSize)
            .WithMessage($"PageSize deve ser no máximo {PaginationParameters.MaxPageSize}.");
    }
}
