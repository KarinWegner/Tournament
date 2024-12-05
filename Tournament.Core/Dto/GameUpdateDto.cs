using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public record GameUpdateDto
    {
        public string? Title { get; set; }
        public DateTime? Time { get; set; }
    }
}
