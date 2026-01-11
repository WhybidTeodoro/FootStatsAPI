using FootStatsAPI.DTOs.Match;

namespace FootStatsAPI.Services.Interfaces;

/// <summary>
/// Interface para implementação das regras de negócio para a entidade Match
/// </summary>
public interface IMatchService
{
    /// <summary>
    /// Adiciona uma partida a um time do usuario
    /// </summary>
    public Task<MatchResponseDto> AddMatchAsync(int userId, CreateMatchDto dto);

    /// <summary>
    /// Retorna uma partida de um time do usuario
    /// </summary>
    public Task<MatchResponseDto> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Atualiza uma partida de um time do usuario
    /// </summary>
    public Task<MatchResponseDto> UpdateMatchAsync(int userId, int id, UpdateMatchDto dto);

    /// <summary>
    /// Deleta uuma partida de um time do usuario
    /// </summary>
    public Task DeleteMatchAsync(int userId, int id);
}
