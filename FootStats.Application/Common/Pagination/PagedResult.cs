namespace FootStats.Application.Common.Pagination;

/// <summary>
/// Representa um resultado paginado (itens + metadados).
/// </summary>
public sealed class PagedResult<T>
{
    /// <summary>
    /// Itens da página atual.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// Número da página atual (1-based). Ex.: 1 = primeira página.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Quantidade de itens por página.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Quantidade total de itens disponíveis (sem paginação).
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Quantidade total de páginas, calculada a partir de TotalCount e PageSize.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Indica se existe página anterior.
    /// </summary>
    public bool HasPrevious => PageNumber > 1;

    /// <summary>
    /// Indica se existe próxima página.
    /// </summary>
    public bool HasNext => PageNumber < TotalPages;

    private PagedResult(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items), "Items não pode ser null.");

        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "PageNumber deve ser >= 1.");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize deve ser >= 1.");

        if (totalCount < 0)
            throw new ArgumentOutOfRangeException(nameof(totalCount), "TotalCount não pode ser negativo.");

        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;

        TotalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    /// <summary>
    /// Factory method para criar um PagedResult. Ajuda a manter consistência e encapsula as regras.
    /// </summary>
    public static PagedResult<T> Create(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
        => new(items, pageNumber, pageSize, totalCount);

    /// <summary>
    /// Resultado vazio padronizado (caso não tiver itens).
    /// </summary>
    public static PagedResult<T> Empty(int pageNumber, int pageSize)
        => new(Array.Empty<T>(), pageNumber, pageSize, 0);
}
