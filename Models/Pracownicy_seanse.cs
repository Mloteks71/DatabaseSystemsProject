
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Pracownicy_seanse
    {
        public int PracownicyId { get; set; }
        public Pracownicy Pracownicy { get; set; }

        public int SeanseId { get; set; }
        public Seanse Seanse { get; set; }
    }
}
