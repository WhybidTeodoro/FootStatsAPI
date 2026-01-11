using FootStatsAPI.DTOs.Player;

namespace FootStatsAPI.Services.Interfaces;

/// <summary>
/// Interface para implementação de regras de negócio para a entidade Player
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Adiciona um jogador a um time do usuario
    /// </summary>
    public Task<PlayerResponseDto> AddPlayerAsync(int userId, CreatePlayerDto dto);

    /// <summary>
    /// Retorna um jogador de um time do usuario
    /// </summary>
    public Task<PlayerResponseDto> GetByIdAsync(int userId, int id);

    /// <summary>
    /// Atualiza o perfil de um jogador registrado em um time do usuario
    /// </summary>
    public Task<PlayerResponseDto> UpdatePlayerProfileAsync(int userId, int id, UpdatePlayerProfileDto dto);

    /// <summary>
    /// Atualiza as estatisticas de um jogador registrado em um time do usuario
    /// </summary>
    public Task<PlayerResponseDto> UpdatePlayerStatsAsync(int userId, int id, UpdatePlayerStatsDto dto);

    /// <summary>
    /// Deleta um jogador registrado de um time do usuario 
    /// </summary>
    public Task DeletePlayerAsync(int userId, int id);
}
