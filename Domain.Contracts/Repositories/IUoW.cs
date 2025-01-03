﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IUoW
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }
        ITournamentConstraints TournamentConstraints { get; }

        Task CompleteAsync();
    }
}
