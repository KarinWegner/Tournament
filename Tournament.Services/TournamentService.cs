using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Tournament.Core.Dto;

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
    }
    

}