using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Gatunki_filmy
    {
        public int GatunkiId { get; set; }
        public Gatunki Gatunki{ get; set; }

        public int FilmyId { get; set; }
        public Filmy Filmy { get; set; }
    }
}
