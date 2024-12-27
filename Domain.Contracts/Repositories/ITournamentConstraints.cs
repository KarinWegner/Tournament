﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ITournamentConstraints
    {
        bool ExceedsAllowedGameCount(int numberOfGames);
    }
}