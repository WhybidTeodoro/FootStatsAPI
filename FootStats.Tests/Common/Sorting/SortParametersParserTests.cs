using FluentAssertions;
using FootStats.Application.Common.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootStats.Tests.Common.Sorting;

public class SortParametersParserTests
{
    [Fact]
    public void ParseOrDefault_DeveRetornarAsc_QuandoSortDirectionForNull()
    {
        // Arrange
        string? sortDirection = null;

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Asc);
    }

    [Fact]
    public void ParseOrDefault_DeveRetornarAsc_QuandoSortDirectionForVazio()
    {
        // Arrange
        var sortDirection = string.Empty;

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Asc);
    }

    [Fact]
    public void ParseOrDefault_DeveRetornarAsc_QuandoSortDirectionForAsc()
    {
        // Arrange
        var sortDirection = "asc";

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Asc);
    }

    [Fact]
    public void ParseOrDefault_DeveRetornarDesc_QuandoSortDirectionForDesc()
    {
        // Arrange
        var sortDirection = "desc";

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Desc);
    }

    [Fact]
    public void ParseOrDefault_DeveRetornarDesc_QuandoSortDirectionForDescEmMaiusculo()
    {
        // Arrange
        var sortDirection = "DESC";

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Desc);
    }

    [Fact]
    public void ParseOrDefault_DeveRetornarAsc_QuandoSortDirectionForInvalido()
    {
        // Arrange
        var sortDirection = "qualquercoisa";

        // Act
        var result = SortParametersParser.ParseOrDefault(sortDirection);

        // Assert
        result.Should().Be(SortDirection.Asc);
    }
}
