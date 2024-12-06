using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IGameRepository
    {
        public Task<IEnumerable<Game>> GetAllAsync(int tournamentdetailsId, bool trackChanges = false);
        public Task<Game> GetAsync(int id, bool trackChanges = false);

        public Task<bool> AnyAsync(int id, bool trackChanges = false);

        public void Add(Game game);
        public void Update(Game Game);
        public void Remove(Game game);
    }
}
