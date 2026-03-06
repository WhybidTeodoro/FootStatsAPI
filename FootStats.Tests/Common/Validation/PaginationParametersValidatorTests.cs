using FluentValidation.TestHelper;
using FootStats.Application.Common.Pagination;
using FootStats.Application.Validators.Pagination;

namespace FootStats.Tests.Common.Validation;

public class PaginationParametersValidatorTests
{
    private readonly PaginationParametersValidator _validator;

    public PaginationParametersValidatorTests()
    {
        _validator = new PaginationParametersValidator();
    }

    [Fact]
    public void DevePassar_QuandoParametrosSaoValidos()
    {
        // Arrange
        var parameters = new PaginationParameters
        {
            PageNumber = 1,
            PageSize = 20
        };

        // Act
        var result = _validator.TestValidate(parameters);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void DeveFalhar_QuandoPageNumberForMenorQue1()
    {
        // Arrange
        var parameters = new PaginationParameters
        {
            PageNumber = 0,
            PageSize = 10
        };

        // Act
        var result = _validator.TestValidate(parameters);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public void DeveFalhar_QuandoPageSizeForMenorQue1()
    {
        // Arrange
        var parameters = new PaginationParameters
        {
            PageNumber = 1,
            PageSize = 0
        };

        // Act
        var result = _validator.TestValidate(parameters);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public void DeveFalhar_QuandoPageSizeForMaiorQueMaximo()
    {
        // Arrange
        var parameters = new PaginationParameters
        {
            PageNumber = 1,
            PageSize = 200
        };

        // Act
        var result = _validator.TestValidate(parameters);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}