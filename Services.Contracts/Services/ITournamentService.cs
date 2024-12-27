using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Request;

namespace Services.Contracts.Services
{
    public interface ITournamentService
    {
        Task DeleteTournament(int id);
        Task<(IEnumerable<TournamentDto>, MetaData metaData)> GetTournamentsAsync(TournamentRequestParams requestParams, bool trackChanges = false);
        Task<TournamentDto> GetTournamentsAsync(int id, bool trackChanges = false);
        Task PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument);
        Task<TournamentDto> PostTournament(TournamentCreateDto dto);
        Task<TournamentDto> PutTournamentAsync(int id, TournamentUpdateDto tournamentDetails);
    }
}
