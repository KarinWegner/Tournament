using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data.Repositories;
using AutoMapper;
using Tournament.Core.Dto;

namespace Tournament.Api.Controllers
{
    [Route("api/tournaments/{tournamentdetailsId}/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public GamesController(TournamentApiContext context, IUoW uow, IMapper mapper)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(int tournamentdetailsId)
        {

            var tournament = await _uow.tournamentRepository.GetAsync(tournamentdetailsId);
            if (tournament == null)
            {
                return NotFound(tournament);
            }
            var tournamentDto = _mapper.Map<TournamentDetails>(tournament);

            var games = await _uow.gameRepository.GetAllAsync(tournamentdetailsId);
            var gamesDto = _mapper.Map<IEnumerable<Game>>(games);
            return Ok(gamesDto);
        }

        // GET: api/Games/5
        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> GetGame(int tournamentdetailsId, int gameId)
        {
            //var tournament = _uow.tournamentRepository.GetAsync(tournamentdetailsId);
            //if (tournament ==null)
            //{
            //    return NotFound(tournament);
            //}
            //var tournamentDto = _mapper.Map<TournamentDetails>(tournament);
              var game = await _uow.gameRepository.GetAsync(gameId);
            
            if (game == null)
            {
                return NotFound();
            }

            if (!await _uow.gameRepository.AnyAsync(gameId, tournamentdetailsId))
            {
                return NotFound();
            }
          
            var gameDto = _mapper.Map<Game>(game);

            return gameDto;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gameId}")]
        public async Task<IActionResult> PutGame(int tournamentdetailsId, int gameId, GameUpdateDto gameDto)
        {

            var tournament = await _uow.tournamentRepository.GetAsync(tournamentdetailsId);
            if (tournament == null) return NotFound();
            
            var game = await _uow.gameRepository.GetAsync(gameId);
            if (game==null) return NotFound();
            
            if (game.TournamentDetailsId != tournamentdetailsId)
            {
                return BadRequest();
            }

             _mapper.Map(game, gameDto);

            var updatedGame = _mapper.Map<GameDto>(game);

            await _uow.CompleteAsync();

            return Ok(updatedGame);

           
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto gameDto, int tournamentdetailsId)
        {
            if (gameDto.TournamentDetailsId != tournamentdetailsId)
            {
                return BadRequest();
            }

            var game = _mapper.Map<Game>(gameDto);          
            _uow.gameRepository.Add(game);

            await _uow.CompleteAsync();
            var createdGame = _mapper.Map<GameDto>(game);
            return CreatedAtAction("GetGame", new { tournamentdetailsId =createdGame.TournamentDetailsId , gameId = createdGame.Id }, createdGame);
        }

        // DELETE: api/Games/5
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame(int gameId, int tournamentdetailsId)
        {
            var tournament = await _uow.tournamentRepository.GetAsync(tournamentdetailsId);
            if (tournament == null) return NotFound();

            var game = await _uow.gameRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            if (game.TournamentDetailsId != tournamentdetailsId)
            {
                return BadRequest();
            }
            
            _uow.gameRepository.Remove(game);
            await _uow.CompleteAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
