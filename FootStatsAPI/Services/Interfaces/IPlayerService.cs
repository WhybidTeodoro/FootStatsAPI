using FootStatsAPI.DTOs.Player;

namespace FootStatsAPI.Services.Interfaces;

public interface IPlayerService
{
    public Task<PlayerResponseDto> AddPlayerAsync(int userId, CreatePlayerDto dto);

    public Task<PlayerResponseDto> GetByIdAsync(int id, int userId);

    public Task<PlayerResponseDto> UpdatePlayerProfileAsync(int id, int userId, UpdatePlayerProfileDto dto);

    public Task<PlayerResponseDto> UpdatePlayerStatsAsync(int id, int userId, UpdatePlayerStatsDto dto);

    public Task DeletePlayerAsync(int id, int userId);
}
