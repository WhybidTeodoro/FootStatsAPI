namespace FootStats.Application.Common.Sorting
{
    /// <summary>
    /// Converte string da API ("asc"/"desc") para enum.
    /// Mantém código simples e centralizado.
    /// </summary>
    public static class SortParametersParser
    {
        public static SortDirection ParseOrDefault(string? sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortDirection))
                return SortDirection.Asc;

            return sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? SortDirection.Desc
                : SortDirection.Asc;
        }
    }

}
