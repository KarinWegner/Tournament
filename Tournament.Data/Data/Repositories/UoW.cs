using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;

namespace Tournament.Data.Data.Repositories
{
    internal class UoW : IUoW
    {
        public ITournamentRepository tournamentRepository { get;  }

        public IGameRepository gameRepository { get; }

        private readonly TournamentApiContext _context;

        public UoW (TournamentApiContext context)
        {
            _context = context;
            tournamentRepository = new TournamentRepository(_context);
            gameRepository = new GameRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
