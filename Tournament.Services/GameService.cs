using AutoMapper;
using Domain.Contracts.Repositories;
using Services.Contracts.Services;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private IUoW uow;
        private readonly IMapper mapper;

        public GameService(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> GetGames(int tournamentdetailsId)
        {
            var tournament = await uow.TournamentRepository.GetAsync(tournamentdetailsId);
            if (tournament == null)
            {
                //Todo add exception
                //return NotFound(tournament);
            }
            var tournamentDto = mapper.Map<TournamentDetails>(tournament);

            var games = await uow.GameRepository.GetAllAsync(tournamentdetailsId);
            return  mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task<GameDto> GetGame(int tournamentdetailsId, int gameId)
        {
            var tournament = await uow.TournamentRepository.AnyAsync(tournamentdetailsId);
            //Todo: add exceptions
            //if (!tournament) return NotFound("Tournament was not found");

            var game = await uow.GameRepository.GetAsync(gameId);
            //if (game == null) return NotFound("Game was not found");

            //if (game.TournamentDetailsId != tournamentdetailsId)
            //    return BadRequest("Game is not part of selected Tournament");


            //if (!await _uow.gameRepository.AnyAsync(gameId, tournamentdetailsId))
            //    return NotFound("Game was not found in tournament database");

            return mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> PutGame(int tournamentdetailsId, int gameId, GameUpdateDto gameDto)
        {
            var tournament = await uow.TournamentRepository.AnyAsync(tournamentdetailsId);
           // if (!tournament) return NotFound("Tournament was not found");

            var game = await uow.GameRepository.GetAsync(gameId);
           // if (game == null) return NotFound("Game was not found");

            //if (game.TournamentDetailsId != tournamentdetailsId)
            //    return BadRequest("Game is not part of selected Tournament");

            mapper.Map(game, gameDto);

            var updatedGame = mapper.Map<GameDto>(game);

            await uow.CompleteAsync();
            return updatedGame;
        }

        public async Task<GameDto> PostGame(GameCreateDto gameDto, int tournamentdetailsId)
        {
            if (gameDto.TournamentDetailsId != tournamentdetailsId)
            {
              //  return BadRequest("Game is not part of selected Tournament");
            }

            var game = mapper.Map<Game>(gameDto);
            uow.GameRepository.Add(game);

            await uow.CompleteAsync();
            return mapper.Map<GameDto>(game);
        }

        public async Task PatchGame(int tournamentdetailsId, int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            //if (patchDocument is null) return BadRequest("No patch document found");

            var tournament = await uow.TournamentRepository.AnyAsync(tournamentdetailsId);
           // if (!tournament) return NotFound("Tournament not found in database");

            var gameToPatch = await uow.GameRepository.GetAsync(gameId);
            //if (gameToPatch == null) return NotFound("Game not found");

            //if (gameToPatch.TournamentDetailsId != tournamentdetailsId) return BadRequest("Game is not part of selected tournament");

            var gameDto = mapper.Map<GameUpdateDto>(gameToPatch);

            patchDocument.ApplyTo(gameDto);
            mapper.Map(gameDto, gameToPatch);
            await uow.CompleteAsync();
        }

        public async Task DeleteGame(int gameId, int tournamentdetailsId)
        {
            var tournament = await uow.TournamentRepository.AnyAsync(tournamentdetailsId);
            //if (!tournament) return NotFound("Tournament was not found");

            var game = await uow.GameRepository.GetAsync(gameId);
           // if (game == null) return NotFound("Game was not found");

            //if (game.TournamentDetailsId != tournamentdetailsId)
            //    return BadRequest("Game is not part of selected Tournament");

            uow.GameRepository.Remove(game);
            await  uow.CompleteAsync();
        }
    }
}