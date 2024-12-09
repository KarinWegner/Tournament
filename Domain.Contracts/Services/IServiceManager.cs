using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IServiceManager
    {
       IGameService GameService { get; }
        ITournamentService TournamentService { get; }
    }
}
