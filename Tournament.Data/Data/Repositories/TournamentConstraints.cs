using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Data.Data.Repositories
{
    public class TournamentConstraints : ITournamentConstraints
    {
        private const int MaxAllowedGames = 10;
        public bool ExceedsAllowedGameCount(int numberOfGames)
        {
            return MaxAllowedGames >= numberOfGames;
        }
    }
}
