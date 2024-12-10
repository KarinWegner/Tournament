using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Domain.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGames(int tournamentdetailsId);
        Task<GameDto> GetGame(int tournamentdetailsId, int gameId);
        Task<GameDto> PutGame(int tournamentdetailsId, int gameId, GameUpdateDto gameDto);
        Task<GameDto> PostGame(GameCreateDto gameDto, int tournamentdetailsId);
        Task PatchGame(int tournamentdetailsId, int gameId, JsonPatchDocument<GameUpdateDto> patchDocument);
        Task DeleteGame(int gameId, int tournamentdetailsId);
    }
}
