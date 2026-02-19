namespace FootStats.Application.Common.Pagination;

// <summary>
/// Parâmetros usados internamente pela Application.
/// Independente de HTTP.
/// </summary>
public class PaginationParameters
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 20;

    public const int MinPageNumber = 1;
    public const int MinPageSize = 1;
    public const int MaxPageSize = 100;

    public int PageNumber { get; set; } = DefaultPageNumber;
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// Quantidade de registros que o EF Core deve pular.
    /// </summary>
    public int GetSkipCount()
    {
        return (PageNumber - 1) * PageSize;
    }
}
