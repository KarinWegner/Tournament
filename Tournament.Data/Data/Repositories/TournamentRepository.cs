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
    internal class TournamentRepository : RepositoryBase<TournamentDetails>, ITournamentRepository
    {       
        public TournamentRepository(TournamentApiContext context) : base(context) { }
        

        public async Task<bool> AnyAsync(int id, bool trackChanges = false)
        {
            return await FindByCondition((t => t.Id == id), trackChanges).AnyAsync();
        }        

        public async Task<TournamentDetails> GetAsync(int id, bool trackChanges = false)
        {            
            return await FindByCondition(t => t.Id == id, trackChanges).FirstOrDefaultAsync();            
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeMatches = false, bool trackChanges = false)
        {
            //Använder FindAll funktionen i RepositoryBase
            return includeMatches ? await FindAll(trackChanges).Include(t => t.Games).ToListAsync() :
                                    await FindAll(trackChanges).ToListAsync();            
        }




    }
}
