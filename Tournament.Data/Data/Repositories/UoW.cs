using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Data.Data.Repositories
{
    public class UoW : IUoW
    {

        private readonly TournamentApiContext _context;
        private readonly Lazy<ITournamentRepository> tournamentRepository;
        private readonly Lazy<IGameRepository> gameRepository;
        public ITournamentRepository TournamentRepository => tournamentRepository.Value;
        public IGameRepository GameRepository => gameRepository.Value;


        public UoW (TournamentApiContext context, Lazy<ITournamentRepository> tournamentrepository, Lazy<IGameRepository> gamerepository)
        {
            _context = context;
            tournamentRepository = tournamentRepository;
            gameRepository = gameRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
