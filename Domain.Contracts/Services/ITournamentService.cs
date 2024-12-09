using Azure;
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
    public interface ITournamentService
    {
        Task DeleteTournament(int id);
        Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false);
        Task<TournamentDto> GetTournamentsAsync(int id, bool trackChanges = false);
        Task PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument);
        Task<TournamentDto> PostTournament(TournamentCreateDto dto);
        Task<TournamentDto> PutTournamentAsync(int id, TournamentUpdateDto tournamentDetails);
    }
}
