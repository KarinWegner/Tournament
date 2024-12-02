using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Data.Data.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private readonly TournamentApiContext _context;

        public GameRepository(TournamentApiContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Game game)
        {
            _context.Game.Add(game);
        }

        public Task<bool> AnyAsync(int id)
        {
            return _context.Game.AnyAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _context.Game.FirstOrDefaultAsync(g => g.Id == id);
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
        }

        public void Update(Game Game)
        {
            throw new NotImplementedException();
        }
    }
}
