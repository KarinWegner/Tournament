using AutoMapper;
using Domain.Contracts.Repositories;
using Services.Contracts.Services;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;
using Tournament.Core.Response;

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

        public async Task<ApiBaseResponse> GetGames(int tournamentdetailsId)
        {
            var tournament = await uow.TournamentRepository.GetAsync(tournamentdetailsId);
            if (tournament == null)
            {
                return new TournamentNotFoundResponse(tournamentdetailsId);
            }
            var tournamentDto = mapper.Map<TournamentDetails>(tournament);

            var games = await uow.GameRepository.GetAllAsync(tournamentdetailsId);
            var gamesDto = mapper.Map<IEnumerable<GameDto>>(games);

            return new ApiOkResponse<IEnumerable<GameDto>>(gamesDto);
        }

        public async Task<ApiBaseResponse> GetGame(int tournamentdetailsId, int gameId)
        {
            var tournament = await uow.TournamentRepository.AnyAsync(tournamentdetailsId);

            if (!tournament) return new TournamentNotFoundResponse(tournamentdetailsId);

            var game = await uow.GameRepository.GetAsync(gameId);
            if (game == null) return new GameNotFoundResponse(gameId);

            if (game.TournamentDetailsId != tournamentdetailsId)
                return new BadRequestResponse("Game is not part of selected Tournament");



            var gameDto = mapper.Map<GameDto>(game);
            return new ApiOkResponse<GameDto>(gameDto);
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

        public async Task<ApiBaseResponse> PostGame(GameCreateDto gameDto, int tournamentdetailsId)
        {
            if (gameDto.TournamentDetailsId != tournamentdetailsId)
            {
                return new BadRequestResponse("Game is not part of selected Tournament");
            }
            var gameList = await uow.GameRepository.GetAllAsync(tournamentdetailsId);


            bool maxGameCountReached = uow.TournamentConstraints.ExceedsAllowedGameCount(gameList.Count());
            if (maxGameCountReached)
            {
                return new BadRequestResponse("A tournament can not contain more than 10 games.");
            }

            var game = mapper.Map<Game>(gameDto);
            uow.GameRepository.Add(game);

            await uow.CompleteAsync();
               var addedGame = mapper.Map<GameDto>(game);

            return new ApiCreatedAtResponse<GameDto>(addedGame);
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
            if (game == null) throw new GameNotFoundException(gameId);

            //if (game.TournamentDetailsId != tournamentdetailsId)
            //    return BadRequest("Game is not part of selected Tournament");

            uow.GameRepository.Remove(game);
            await  uow.CompleteAsync();
        }
        public async Task<bool> GameCreationAllowed(int tournamentdetailsId)
        {
            var tournamentToCount = await uow.TournamentRepository.GetAsync(tournamentdetailsId);
            if (tournamentToCount == null)
            {
                throw new TournamentNotFoundException(tournamentdetailsId);
            }
            var gameCount = await uow.TournamentRepository.CountAsync(tournamentdetailsId);

            return !uow.TournamentConstraints.ExceedsAllowedGameCount(gameCount);
        }
    }
}