using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    [Table("Klienci")]
    public class Klienci
    { 
        [Key]
        public int Id { get; set; }
        public int nr_telefonu { get; set; }
        [Required]
        [Display(Name = "Hasło")]
        public string haslo { get; set; }
        [StringLength(16)]
        [Display(Name = "Imie")]
        public string imie { get; set; }
        [StringLength(32)]
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }
        public ICollection<Bilety> Biletys { get; set; }
       
    }
}