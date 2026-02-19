namespace FootStats.Application.Common.Pagination
{
     /// <summary>
     /// Representa um resultado paginado (itens + metadados).
     /// Contrato simples para uso em Services e Controllers.
     /// </summary>
    public class PagedResult<T>
    {
        /// <summary>
        /// Itens da página atual.
        /// </summary>
        public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

        /// <summary>
        /// Número da página atual (1-based). Ex.: 1 = primeira página.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Quantidade de itens por página.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Quantidade total de itens disponíveis (sem paginação).
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Quantidade total de páginas, calculada a partir de TotalCount e PageSize.
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (PageSize <= 0)
                    return 0;

                return TotalCount <= 0
                    ? 0
                    : (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }

        /// <summary>
        /// Indica se existe página anterior.
        /// </summary>
        public bool HasPrevious => PageNumber > 1;

        /// <summary>
        /// Indica se existe próxima página.
        /// </summary>
        public bool HasNext => PageNumber < TotalPages;

        /// <summary>
        /// Helper simples para criar um resultado paginado já preenchido.
        /// (Não é obrigatório usar, mas reduz repetição no código.)
        /// </summary>
        public static PagedResult<T> From(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
        {
            return new PagedResult<T>
            {
                Items = items ?? Array.Empty<T>(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
