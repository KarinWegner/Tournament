﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public string Title { get; set; }
        protected NotFoundException(string message, string title = "Not Found") : base(message)
        {
            Title = title;
        }
    }

    public class TournamentNotFoundException : NotFoundException
    {
        public TournamentNotFoundException(int id) : base($"The tournament with id {id} was not found")
        {
        }
    }

    public class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(int id) : base($"The game with id {id} was not found")
        {
        }
    }
}