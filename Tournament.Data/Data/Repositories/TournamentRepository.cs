using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Data.Data.Repositories
{
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;

            public TournamentRepository(TournamentApiContext context) {
                 _context = context ?? throw new ArgumentNullException(nameof(context));
            }
        public void Add(TournamentDetails tournament)
        {
            _context.TournamentDetails.Add(tournament);
        }

        public Task<bool> AnyAsync(int id)
        {
            return _context.TournamentDetails.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync()
        {
            return await _context.TournamentDetails.ToListAsync();
        }

        public async Task<TournamentDetails> GetAsync(int id)
        {

            return await _context.TournamentDetails.FindAsync(id);
        }

        public void Remove(TournamentDetails tournament)
        {
            _context.Remove(tournament);
        }

        public void Update(TournamentDetails tournament)
        {
            throw new NotImplementedException();
        }
    }
}
