using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Data.Data.Repositories;
using AutoMapper;
using Tournament.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Entities;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;

namespace Tournament.Api.Controllers
{
    [Route("api/tournaments/{tournamentdetailsId}/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames(int tournamentdetailsId)
        {
            IEnumerable<GameDto> gamesDto = await serviceManager.GameService.GetGames(tournamentdetailsId);          
            return Ok(gamesDto);
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDto>> GetGame(int tournamentdetailsId, int gameId)
        {
            GameDto gameDto = await serviceManager.GameService.GetGame(tournamentdetailsId, gameId);      
            return gameDto;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gameId}")]
        public async Task<IActionResult> PutGame(int tournamentdetailsId, int gameId, GameUpdateDto gameDto)
        {
            GameDto updatedGame = await serviceManager.GameService.PutGame(tournamentdetailsId, gameId, gameDto);            
            return Ok(updatedGame);
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto gameDto, int tournamentdetailsId)
        {
            GameDto createdGame = await serviceManager.GameService.PostGame(gameDto, tournamentdetailsId);           
            return CreatedAtAction("GetGame", new { tournamentdetailsId = createdGame.TournamentDetailsId, gameId = createdGame.Id }, createdGame);
        }

        // PATCH: api/tournaments/1/games/5
        [HttpPatch("{gameId}")]
        public async Task<IActionResult> PatchGame(int tournamentdetailsId, int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            await serviceManager.GameService.PatchGame(tournamentdetailsId,gameId,patchDocument);
            return NoContent();
        }


        // DELETE: api/Games/5
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame(int gameId, int tournamentdetailsId)
        {
            await serviceManager.GameService.DeleteGame(gameId,tournamentdetailsId);         
            return NoContent();
        }


    }
}
