﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Repositories
{
    public interface IUoW
    {
         ITournamentRepository tournamentRepository { get; }
        IGameRepository gameRepository { get; }

        Task CompleteAsync();
    }
}