using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        public IUoW uow;
        private readonly IMapper mapper;

        public TournamentService(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<TournamentDto>> GetTournamentsAsync(bool includeGames, bool trackChanges = false) 
        {

            return mapper.Map<IEnumerable<TournamentDto>>(await uow.tournamentRepository.GetAllAsync(includeGames, trackChanges));

        }
        public async Task<TournamentDto> GetTournamentsAsync(int id, bool trackChanges = false)
        {
            var tournament = await uow.tournamentRepository.GetAsync(id);
            //ToDo add exception
           // if (tournament == null) return NotFound();
            var tournamentDto = mapper.Map<TournamentDto>(tournament);

            return tournamentDto;
        }

        public async Task<TournamentDto> PutTournamentAsync(int id, TournamentUpdateDto tournamentDetails)
        {
            //ToDo: add exceptions
            //if (id != tournamentDetails.Id)  return BadRequest();
            var tournament = await uow.tournamentRepository.GetAsync(id, trackChanges: true);

            //if (tournament == null) return NotFound("Tournament was not found");
            //if (id != tournament.Id) return BadRequest("Tournament Id does not match input tournamnet");

            mapper.Map(tournamentDetails, tournament);

            await uow.CompleteAsync();
            return mapper.Map<TournamentDto>(tournament);
        }

        public async Task<TournamentDto> PostTournament(TournamentCreateDto dto)
        {
            var tournamentDetails = mapper.Map<TournamentDetails>(dto);

            uow.tournamentRepository.Add(tournamentDetails);
            await uow.CompleteAsync();

            return mapper.Map<TournamentDto>(tournamentDetails);
        }

        public async Task DeleteTournament(int id)
        {
            var tournamentDetails = await uow.tournamentRepository.GetAsync(id);

            //ToDo: Add exceptions
            //if (tournamentDetails == null) return NotFound("Tournament was not found");

            var games = await uow.gameRepository.GetAllAsync(id);

           // if (games.Count() != 0) return BadRequest("Can not delete tournament with existing games");

            uow.tournamentRepository.Remove(tournamentDetails);

            await uow.CompleteAsync();
        }

        public async Task PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            //ToDo Add exceptions
            //if (patchDocument is null) return BadRequest("No patch document found");

            var tournamentToPatch = await uow.tournamentRepository.GetAsync(id);
           // if (tournamentToPatch == null) return NotFound("Tournament was not found");

            var tournamentDto = mapper.Map<TournamentUpdateDto>(tournamentToPatch);

            patchDocument.ApplyTo(tournamentDto);

            mapper.Map(tournamentDto, tournamentToPatch);
            await uow.CompleteAsync();
        }
    }
    

}