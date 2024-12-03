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
    [Route("api/Tournaments/{tournamentdetailsId}/Games")]
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
            var gameDto = _mapper.Map<Game>(game);

            if (game == null)
            {
                return NotFound();
            }

            return gameDto;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{tournamentId}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
