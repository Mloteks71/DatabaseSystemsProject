using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace webapp.Models
{
    [Table("Gatunki")]
    public class Gatunki
    {
        [Key]
        public int gatunek_id { get; set; }
        [Required]
        [StringLength(32)]
        [Display(Name = "Nazwa")]
        public string nazwa { get; set; }

        public ICollection<Gatunki_filmy> Filmys { get; set; }
    }
}
