using AutoMapper;
using Domain.Contracts.Repositories;
using Services.Contracts.Services;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;
using Tournament.Core.Request;
using Tournament.Core.Response;

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
        public async Task<(IEnumerable<TournamentDto>, MetaData metaData)> GetTournamentsAsync(TournamentRequestParams requestParams, bool trackChanges = false) 
        {

                var pagedList = await uow.TournamentRepository.GetAllAsync(requestParams, trackChanges);
            var TournamentsDto = mapper.Map<IEnumerable<TournamentDto>>(pagedList.Items);
            return (TournamentsDto, pagedList.MetaData);

        }
        public async Task<TournamentDto> GetTournamentsAsync(int id, bool trackChanges = false)
        {
            var tournament = await uow.TournamentRepository.GetAsync(id);
          
            if (tournament == null)
                throw new TournamentNotFoundException(id);

            var tournamentDto = mapper.Map<TournamentDto>(tournament);

            return tournamentDto;
        }

        public async Task<TournamentDto> PutTournamentAsync(int id, TournamentUpdateDto tournamentDetails)
        {
            //ToDo: add exceptions
            //if (id != tournamentDetails.Id)  return BadRequest();
            var tournament = await uow.TournamentRepository.GetAsync(id, trackChanges: true);

            //if (tournament == null) return NotFound("Tournament was not found");
            //if (id != tournament.Id) return BadRequest("Tournament Id does not match input tournamnet");

            mapper.Map(tournamentDetails, tournament);

            await uow.CompleteAsync();
            return mapper.Map<TournamentDto>(tournament);
        }

        public async Task<TournamentDto> PostTournament(TournamentCreateDto dto)
        {
            var tournamentDetails = mapper.Map<TournamentDetails>(dto);

            uow.TournamentRepository.Add(tournamentDetails);
            await uow.CompleteAsync();

            return mapper.Map<TournamentDto>(tournamentDetails);
        }

        public async Task DeleteTournament(int id)
        {
            var tournamentDetails = await uow.TournamentRepository.GetAsync(id);

            //ToDo: Add exceptions
            //if (tournamentDetails == null) return NotFound("Tournament was not found");

            var games = await uow.GameRepository.GetAllAsync(id);

           // if (games.Count() != 0) return BadRequest("Can not delete tournament with existing games");

            uow.TournamentRepository.Remove(tournamentDetails);

            await uow.CompleteAsync();
        }

        public async Task PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            //ToDo Add exceptions
            //if (patchDocument is null) return BadRequest("No patch document found");

            var tournamentToPatch = await uow.TournamentRepository.GetAsync(id);
           // if (tournamentToPatch == null) return NotFound("Tournament was not found");

            var tournamentDto = mapper.Map<TournamentUpdateDto>(tournamentToPatch);

            patchDocument.ApplyTo(tournamentDto);

            mapper.Map(tournamentDto, tournamentToPatch);
            await uow.CompleteAsync();
        }
        public async Task<int> GetGameCount(int id)
        {
            var tournamentToCount = await uow.TournamentRepository.GetAsync(id);
            if (tournamentToCount == null)
            {
                //  return TournamentNotFoundResponse(id);
                throw new TournamentNotFoundException(id);
            }
            return await uow.TournamentRepository.CountAsync(id);

        }
        
    }
    

}