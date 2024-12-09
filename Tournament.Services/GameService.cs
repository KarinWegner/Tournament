using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private IUoW uow;
        private readonly IMapper mapper;

        public GameService(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

    }
}