using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    [Table("Bilety")]
    public class Bilety
    {
        [Key]
        public int bilet_id { get; set; }
        [Required]
        [Display(Name = "Cena")]
        public int cena { get; set; }
      
        [Display(Name = "Klient")]
        public Klienci Klienci { get; set; }

        [Display(Name = "Seans")]
        public Seanse Seanse { get; set; }
       // [Display(Name = "Seans")]
       // public int SeanseId { get; set; }

    }
}
