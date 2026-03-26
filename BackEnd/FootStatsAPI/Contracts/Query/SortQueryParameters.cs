namespace FootStats.API.Contracts.Query;

/// <summary>
/// Parâmetros de ordenação vindos da QueryString.
/// </summary>
public class SortQueryParameters
{
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; } // "asc" ou "desc"

    /// <summary>
    /// Normaliza valores da QueryString para um formato previsível.
    /// - Trim para remover espaços
    /// - Lowercase para comparar sem dor de cabeça
    /// </summary>
    public (string? SortBy, string? SortDirection) ToNormalized()
    {
        var sortBy = string.IsNullOrWhiteSpace(SortBy) ? null : SortBy.Trim();
        var sortDirection = string.IsNullOrWhiteSpace(SortDirection) ? null : SortDirection.Trim().ToLowerInvariant();

        return (sortBy, sortDirection);
    }
}
