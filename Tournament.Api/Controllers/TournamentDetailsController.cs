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
using AutoMapper;
using Tournament.Core.Dto;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Api.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public TournamentDetailsController(TournamentApiContext context, IUoW uow, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails()
        {


            var tournaments = await _uow.tournamentRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(dto);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
        {
            var tournament = await _uow.tournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return tournament;
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto tournamentDetails)
        {
            var tournament = await _uow.tournamentRepository.GetAsync(id);

            if (id != tournament.Id) return BadRequest();
            
            if (tournament == null) return NotFound();

            _mapper.Map(tournamentDetails, tournament);

           

            await _uow.CompleteAsync();
          return Ok(_mapper.Map<TournamentDto>(tournament));

           // return NoContent();

        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        //{

        //}
        //if (tournamentDetails.Title != null)
        //            tournament.Title = tournamentDetails.Title;
        //if (tournamentDetails.StartDate != null)
        //            tournament.StartDate = (DateTime)tournamentDetails.StartDate;


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
            var tournamentDetails = await _context.TournamentDetails.FindAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            var games = await _uow.gameRepository.GetAllAsync(id);

            if (games.Count() != 0)
            {
                return BadRequest();
            }
            _uow.tournamentRepository.Remove(tournamentDetails);

            await _uow.CompleteAsync();

            return NoContent();
        }

        private bool TournamentDetailsExists(int id)
        {
            return _context.TournamentDetails.Any(e => e.Id == id);
        }
    }
}
