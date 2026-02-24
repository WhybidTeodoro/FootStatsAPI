namespace FootStats.Application.Common.Sorting
{
    /// <summary>
    /// Parâmetros de ordenação usados internamente na Application.
    /// </summary>
    public class SortParameters
    {
        /// <summary>
        /// Campo pelo qual ordenar (ex.: "name", "createdAt").
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Direção de ordenação (Asc/Desc).
        /// </summary>
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}
