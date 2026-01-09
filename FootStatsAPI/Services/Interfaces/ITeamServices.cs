using FootStatsAPI.DTOs.Match;
using FootStatsAPI.DTOs.Player;
using FootStatsAPI.DTOs.Team;

namespace FootStatsAPI.Services.Interfaces;

public interface ITeamServices
{
    Task<TeamResponseDto> AddTeamAsync(int userId, CreateTeamDto dto);

    Task<List<TeamResponseDto>> GetAllAsync(int userId);

    Task<TeamResponseDto> GetByIdAsync(int userId, int id);

    Task<List<PlayerResponseDto>> GetAllPlayersByTeamAsync(int userId, int teamId);

    Task<List<MatchResponseDto>> GetAllMatchessByTeamAsync(int userId, int teamId);

    Task<TeamResponseDto> UpdateTeamAsync(int userId,int id, UpdateTeamDto dto);

    Task DeleteTeamAsync(int userId, int id);
}
