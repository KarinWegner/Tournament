using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Response;

namespace Services.Contracts.Services
{
    public interface IGameService
    {
        Task<ApiBaseResponse> GetGames(int tournamentdetailsId);
        Task<ApiBaseResponse> GetGame(int tournamentdetailsId, int gameId);
        Task<GameDto> PutGame(int tournamentdetailsId, int gameId, GameUpdateDto gameDto);
        Task<ApiBaseResponse> PostGame(GameCreateDto gameDto, int tournamentdetailsId);
        Task PatchGame(int tournamentdetailsId, int gameId, JsonPatchDocument<GameUpdateDto> patchDocument);
        Task DeleteGame(int gameId, int tournamentdetailsId);
        Task<bool> GameCreationAllowed(int tournamentdetailsId);
    }
}
