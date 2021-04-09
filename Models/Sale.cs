using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace webapp.Models
{
    [Table("Sale")]
    public class Sale
    {
        [Key]
        public int nr_sali { get; set; }
        [Required]
        [Display(Name = "Ilosc_miejsc")]
        public int ilosc_miejsc { get; set; }
        
        public ICollection<Seanse> Seanses { get; set; }
    }
}
