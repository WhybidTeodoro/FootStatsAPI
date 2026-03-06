using FluentAssertions;
using FootStats.Application.Common.Pagination;

public class PagedResultTests
{
    [Fact]
    public void TotalPages_DeveRetornarZero_QuandoTotalCountForZero()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: Array.Empty<string>(),
            pageNumber: 1,
            pageSize: 10,
            totalCount: 0);

        // Act
        var totalPages = result.TotalPages;

        // Assert
        totalPages.Should().Be(0);
    }

    [Fact]
    public void TotalPages_DeveRetornar1_QuandoTotalCountFor1EPageSizeFor10()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string> { "item1" },
            pageNumber: 1,
            pageSize: 10,
            totalCount: 1);

        // Act
        var totalPages = result.TotalPages;

        // Assert
        totalPages.Should().Be(1);
    }

    [Fact]
    public void TotalPages_DeveRetornar2_QuandoTotalCountFor20EPageSizeFor10()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 1,
            pageSize: 10,
            totalCount: 20);

        // Act
        var totalPages = result.TotalPages;

        // Assert
        totalPages.Should().Be(2);
    }

    [Fact]
    public void TotalPages_DeveRetornar3_QuandoTotalCountFor21EPageSizeFor10()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 1,
            pageSize: 10,
            totalCount: 21);

        // Act
        var totalPages = result.TotalPages;

        // Assert
        totalPages.Should().Be(3);
    }

    [Fact]
    public void HasPrevious_DeveSerFalse_QuandoPageNumberFor1()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 1,
            pageSize: 10,
            totalCount: 30);

        // Act
        var hasPrevious = result.HasPrevious;

        // Assert
        hasPrevious.Should().BeFalse();
    }

    [Fact]
    public void HasPrevious_DeveSerTrue_QuandoPageNumberForMaiorQue1()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 2,
            pageSize: 10,
            totalCount: 30);

        // Act
        var hasPrevious = result.HasPrevious;

        // Assert
        hasPrevious.Should().BeTrue();
    }

    [Fact]
    public void HasNext_DeveSerTrue_QuandoAindaExistiremPaginasSeguintes()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 2,
            pageSize: 10,
            totalCount: 30);

        // Act
        var hasNext = result.HasNext;

        // Assert
        hasNext.Should().BeTrue();
    }

    [Fact]
    public void HasNext_DeveSerFalse_QuandoEstiverNaUltimaPagina()
    {
        // Arrange
        var result = PagedResult<string>.From(
            items: new List<string>(),
            pageNumber: 3,
            pageSize: 10,
            totalCount: 30);

        // Act
        var hasNext = result.HasNext;

        // Assert
        hasNext.Should().BeFalse();
    }
}