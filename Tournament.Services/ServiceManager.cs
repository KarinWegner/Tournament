using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;

        public ITournamentService TournamentService => tournamentService.Value;
        public IGameService GameService => gameService.Value;
        public ServiceManager(IUoW uow, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(nameof(uow));


             tournamentService = new Lazy<ITournamentService>(() => new TournamentService(uow, mapper));
             gameService = new Lazy<IGameService>(() => new GameService(uow, mapper));
        }
    }
}
