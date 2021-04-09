using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    [Table("Pracownicy")]
    public class Pracownicy
    {
        [Key]
        public int Id { get; set; }
        public int nr_telefonu { get; set; }
        [Required]
        [Display(Name = "Hasło")]
        public string haslo { get; set; }
        [Required]
        [StringLength(16)]
        [Display(Name = "Imie")]
        public string imie { get; set; }
        [Required]
        public Boolean isAdmin { get; set; }
        [Required]
        [StringLength(32)]
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }
        [Required]
        [StringLength(32)]
        [Display(Name = "Stanowisko")]
        public string stanowisko { get; set; }

        [Display(Name = "Pensja")]
        public int pensja { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Zatrudnienia")]
        public Nullable<System.DateTime> data_zatrudnienia { get; set; }

        public ICollection<Pracownicy_seanse> Seanses { get; set; }
    }
}
