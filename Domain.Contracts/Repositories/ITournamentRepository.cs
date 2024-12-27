using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Request;

namespace Domain.Contracts.Repositories
{
    public interface ITournamentRepository
    {
        Task<PagedList<TournamentDetails>> GetAllAsync(TournamentRequestParams requestParams, bool trackChanges = false);
        Task<TournamentDetails> GetAsync(int id, bool trackChanges = false);
        Task<bool> AnyAsync(int id, bool trackChanges=false);
        Task<int> CountAsync(int id);
        void Add(TournamentDetails tournament);
        void Update(TournamentDetails tournament);
        void Remove(TournamentDetails tournament);
    }
}
