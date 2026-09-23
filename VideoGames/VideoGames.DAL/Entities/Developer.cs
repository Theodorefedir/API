using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.DAL.Entities
{
    public class Developer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Country { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public List<Game> Games { get; set; } = [];
    }
}
