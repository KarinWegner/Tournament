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

        private readonly Lazy<ITournamentRepository> _tournamentRepository;
        private readonly Lazy<IGameRepository> _gameRepository;
        private readonly TournamentApiContext _context;
        public ITournamentRepository tournamentRepository => _tournamentRepository.Value;
        public IGameRepository gameRepository => _gameRepository.Value;


        public UoW (TournamentApiContext context)
        {
            _context = context;
           _tournamentRepository = new Lazy<ITournamentRepository>(() => new TournamentRepository(context));
            _gameRepository = new Lazy<IGameRepository>(()=> new  GameRepository(context));
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
