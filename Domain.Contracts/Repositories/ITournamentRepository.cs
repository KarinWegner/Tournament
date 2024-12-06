using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeMatches = false, bool trackChanges = false);
        Task<TournamentDetails> GetAsync(int id, bool trackChanges = false);
        Task<bool> AnyAsync(int id, bool trackChanges=false);
        void Add(TournamentDetails tournament);
        void Update(TournamentDetails tournament);
        void Remove(TournamentDetails tournament);
    }
}
