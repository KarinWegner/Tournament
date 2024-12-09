using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using AutoMapper;
using Tournament.Core.Dto;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Entities;
using Domain.Contracts.Repositories;

namespace Tournament.Api.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public TournamentDetailsController(IUoW uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails(bool includeGames)
        {
            var tournaments = includeGames ? _mapper.Map<IEnumerable<TournamentDto>>(await _uow.tournamentRepository.GetAllAsync(true)) :
                                             _mapper.Map<IEnumerable<TournamentDto>>(await _uow.tournamentRepository.GetAllAsync());
            
            var tournamentDto = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
           
            return Ok(tournamentDto);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
        {
            var tournament = await _uow.tournamentRepository.GetAsync(id);
            if (tournament == null) return NotFound();
            var tournamentDto = _mapper.Map<TournamentDto>(tournament);


            return tournamentDto;
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto tournamentDetails)
        {

            if (id != tournamentDetails.Id) return BadRequest();

            var tournament = await _uow.tournamentRepository.GetAsync(id, trackChanges: true);

            if (tournament == null) return NotFound("Tournament was not found");
            if (id != tournament.Id) return BadRequest("Tournament Id does not match input tournamnet");

            _mapper.Map(tournamentDetails, tournament);

            await _uow.CompleteAsync();
            return Ok(_mapper.Map<TournamentDto>(tournament));

            // return NoContent();

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            if (patchDocument is null) return BadRequest("No patch document found");

            var tournamentToPatch = await _uow.tournamentRepository.GetAsync(id);
            if (tournamentToPatch == null) return NotFound("Tournament was not found");

            var tournamentDto = _mapper.Map<TournamentUpdateDto>(tournamentToPatch);

            patchDocument.ApplyTo(tournamentDto);

            _mapper.Map(tournamentDto, tournamentToPatch);
            await _uow.CompleteAsync();

            return NoContent();





        }



        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentCreateDto tournamentDto)
        {
            var tournamentDetails = _mapper.Map<TournamentDetails>(tournamentDto);

            _uow.tournamentRepository.Add(tournamentDetails);
            await _uow.CompleteAsync();
            var createdTournament = _mapper.Map<TournamentDto>(tournamentDetails);

            return CreatedAtAction("GetTournamentDetails", new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            var tournamentDetails = await _uow.tournamentRepository.GetAsync(id);

            if (tournamentDetails == null) return NotFound("Tournament was not found");

            var games = await _uow.gameRepository.GetAllAsync(id);

            if (games.Count() != 0) return BadRequest("Can not delete tournament with existing games");

            _uow.tournamentRepository.Remove(tournamentDetails);

            await _uow.CompleteAsync();

            return NoContent();
        }

        //private bool TournamentDetailsExists(int id)
        //{
        //    return _context.TournamentDetails.Any(e => e.Id == id);
        //}
    }
}
