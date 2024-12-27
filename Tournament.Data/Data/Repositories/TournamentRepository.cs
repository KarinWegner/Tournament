using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Request;

namespace Tournament.Data.Data.Repositories
{
    public class TournamentRepository : RepositoryBase<TournamentDetails>, ITournamentRepository
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
        public async Task<int> CountAsync(int id)
        {
            return await FindByCondition(t => t.Id == id).CountAsync();
        }

        public async Task<PagedList<TournamentDetails>> GetAllAsync(TournamentRequestParams requestParams, bool trackChanges = false)
        {
            //Använder FindAll funktionen i RepositoryBase
            var tournaments = requestParams.IncludeGames ?  FindAll(trackChanges).Include(t => t.Games) :
                                                              FindAll(trackChanges);        
            
            return await PagedList<TournamentDetails>.CreateAsync(tournaments, requestParams.PageNumber, requestParams.PageSize);


        }




    }
}
