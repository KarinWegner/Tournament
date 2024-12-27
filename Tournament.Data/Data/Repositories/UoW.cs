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
        private readonly Lazy<ITournamentConstraints> tournamentConstraints;

        public ITournamentRepository TournamentRepository => tournamentRepository.Value;
        public IGameRepository GameRepository => gameRepository.Value;
        public ITournamentConstraints TournamentConstraints => tournamentConstraints.Value;


        public UoW (TournamentApiContext context, Lazy<ITournamentRepository> tournamentrepository, Lazy<IGameRepository> gamerepository, Lazy<ITournamentConstraints> tournamentconstraints)
        {
            _context = context;
            tournamentRepository = tournamentrepository;
            gameRepository = gamerepository;
            tournamentConstraints = tournamentconstraints;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
