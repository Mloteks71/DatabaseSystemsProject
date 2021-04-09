using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace webapp.Models
{
    [Table("Aktorzy")]
    public class Aktorzy
    {
        [Key]
        public int aktor_id { get; set; }
        [Required]
        [StringLength(16)]
        [Display(Name = "Imie")]
        public string imie { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(16)]
        public string nazwisko { get; set; }
        [Display(Name = "Wiek")]
        public int wiek { get; set; }

        public ICollection<Aktorzy_filmy> Filmys { get; set; }
    }
}