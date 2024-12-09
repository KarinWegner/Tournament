using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Dto;
using Domain.Contracts.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Api.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public TournamentDetailsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails(bool includeGames)
        {
            var tournamentsDto = await serviceManager.TournamentService.GetTournamentsAsync(includeGames);           
            return Ok(tournamentsDto);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
        {
            var tournamentDto = await serviceManager.TournamentService.GetTournamentsAsync(id);
            return tournamentDto;
        }

        // PUT: api/TournamentDetails/5    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto tournamentDetails)
        {          
            var tournamentDto = await serviceManager.TournamentService.PutTournamentAsync(id, tournamentDetails);
            return Ok(tournamentDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            await serviceManager.TournamentService.PatchTournament(id, patchDocument);          
            return NoContent();
        }


        // POST: api/TournamentDetails       
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournamentDetails(TournamentCreateDto dto)
        {
            TournamentDto createdTournament = await serviceManager.TournamentService.PostTournament(dto);           
            return CreatedAtAction("GetTournamentDetails", new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            await serviceManager.TournamentService.DeleteTournament(id);            
            return NoContent();
        }


    }
}
