using FluentAssertions;
using FootStats.Application.Common.Pagination;

namespace FootStats.Tests.Common.Pagination;

public class PaginationParametersTests
{
    [Fact]
    public void GetSkipCount_DeveRetornarZero_QuandoPageNumberFor1()
    {
        // Arrange
        var pagination = new PaginationParameters
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var skipCount = pagination.GetSkipCount();

        // Assert
        skipCount.Should().Be(0);
    }

    [Fact]
    public void GetSkipCount_DeveRetornar10_QuandoPageNumberFor2EPageSizeFor10()
    {
        // Arrange
        var pagination = new PaginationParameters
        {
            PageNumber = 2,
            PageSize = 10
        };

        // Act
        var skipCount = pagination.GetSkipCount();

        // Assert
        skipCount.Should().Be(10);
    }

    [Fact]
    public void GetSkipCount_DeveRetornar20_QuandoPageNumberFor3EPageSizeFor10()
    {
        // Arrange
        var pagination = new PaginationParameters
        {
            PageNumber = 3,
            PageSize = 10
        };

        // Act
        var skipCount = pagination.GetSkipCount();

        // Assert
        skipCount.Should().Be(20);
    }
}
