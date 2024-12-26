using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Data.Data.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(TournamentApiContext context) :base(context) { }
       
        public async Task<bool> AnyAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition((t => t.Id == id), trackChanges).AnyAsync();
        }
       

        public async Task<IEnumerable<Game>> GetAllAsync(int tournamentdetailsId, bool trackChanges = false)
        {
            return await FindByCondition(g => g.TournamentDetailsId == tournamentdetailsId, trackChanges).ToListAsync();
        }

        public async Task<Game> GetAsync(int id, bool trackChanges)
        {
            return await FindByCondition(t => t.Id == id, trackChanges).FirstOrDefaultAsync();
        }

      
    }
}
