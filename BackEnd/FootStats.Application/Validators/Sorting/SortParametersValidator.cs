using FluentValidation;
using FootStats.Application.Common.Sorting;

namespace FootStats.Application.Validators.Sorting
{
    /// <summary>
    /// Validação base: não deixa SortBy vir com lixo (tamanho, etc).
    /// A whitelist por recurso (Players/Teams) é feita em validadores específicos.
    /// </summary>
    public class SortParametersValidator : AbstractValidator<SortParameters>
    {
        public SortParametersValidator()
        {
            RuleFor(x => x.SortBy)
                .MaximumLength(50)
                .WithMessage("SortBy deve ter no máximo 50 caracteres.");
        }
    }
}
