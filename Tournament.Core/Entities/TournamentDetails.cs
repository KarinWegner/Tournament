﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class TournamentDetails
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tournament title is a required field")]
        [MaxLength(40, ErrorMessage = "Maximum title length is 40 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tournament requires a start date")]
        public DateTime StartDate { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
