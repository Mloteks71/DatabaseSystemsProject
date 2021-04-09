using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Aktorzy_filmy
    {
        public int AktorzyId { get; set; }
        public Aktorzy Aktorzy { get; set; }

        public int FilmyId { get; set; }
        public Filmy Filmy { get; set; }
    }
}
