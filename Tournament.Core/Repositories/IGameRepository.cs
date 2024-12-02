using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface IGameRepository 
    {
        public Task<IEnumerable<Game>> GetAllAsync(int tournamentdetailsId);
        public Task<Game> GetAsync(int id);

        public Task<bool> AnyAsync(int id);
        public Task<bool> AnyAsync(int id, int tournamentdetailsId);

        public void Add(Game game);
        public void Update(Game Game);
         public void Remove(Game game);
    }
}
