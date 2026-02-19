namespace FootStats.API.Contracts.Query;

/// <summary>
/// Parâmetros recebidos via QueryString.
/// Ex.: ?pageNumber=2&pageSize=25
/// </summary>
public class PaginationQueryParameters
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    /// <summary>
    /// Aplica valores padrão caso não venham na requisição.
    /// </summary>
    public (int PageNumber, int PageSize) ToNormalized(
        int defaultPageNumber = 1,
        int defaultPageSize = 20)
    {
        return (
            PageNumber ?? defaultPageNumber,
            PageSize ?? defaultPageSize
        );
    }
}