using FluentValidation.TestHelper;
using FootStats.Application.Common.Sorting;
using FootStats.Application.Validators.Sorting;

namespace FootStats.Tests.Common.Validation
{
    public class SortParametersValidatorTests
    {
        private readonly SortParametersValidator _validator;

        public SortParametersValidatorTests()
        {
            _validator = new SortParametersValidator();
        }

        [Fact]
        public void DevePassar_QuandoSortByForValido()
        {
            // Arrange
            var parameters = new SortParameters
            {
                SortBy = "name"
            };

            // Act
            var result = _validator.TestValidate(parameters);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.SortBy);
        }

        [Fact]
        public void DeveFalhar_QuandoSortByForMuitoGrande()
        {
            // Arrange
            var parameters = new SortParameters
            {
                SortBy = new string('a', 200)
            };

            // Act
            var result = _validator.TestValidate(parameters);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SortBy);
        }
    }
}
