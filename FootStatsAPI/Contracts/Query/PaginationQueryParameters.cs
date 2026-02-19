namespace FootStats.API.Contracts.Query;

/// <summary>
/// Representa parâmetros de paginação vindos da QueryString (HTTP).
/// Ex.: ?pageNumber=2&pageSize=25
/// </summary>
/// 
public sealed class PaginationQueryParameters
{
    /// <summary>
    /// Número da página (1-based). Se não vier na URL, usa-se o valor default.
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Tamanho da página. Se não vier na URL, usa-se o valor default.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Normaliza valores nulos para defaults seguros.
    /// </summary>
    public (int PageNumber, int PageSize) ToNormalized(int defaultPageNumber = 1, int defaultPageSize = 20)
    {
        var pageNumber = PageNumber ?? defaultPageNumber;
        var pageSize = PageSize ?? defaultPageSize;

        return (pageNumber, pageSize);
    }
}

