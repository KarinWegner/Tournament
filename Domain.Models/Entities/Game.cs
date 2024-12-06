using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Game title is a required field")]
        [MaxLength(40, ErrorMessage = "Maximum title length is 40 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Game time is a required field")]
        public DateTime Time { get; set; }
        public int TournamentDetailsId { get; set; }
    }
}
