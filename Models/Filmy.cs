using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    [Table("Filmy")]
    public class Filmy
    {
        [Key]
        public int film_id { get; set; }
        [Required]
        [StringLength(32)]
        [Display(Name ="Tytuł")]
        public string tytul { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Data Premiery")]
        public Nullable<System.DateTime> data_premiery { get; set; }
        [Required]
        [Display(Name="Czas trwania ")]
        public int czas_trwania { get; set; }
        [Display(Name="Ograniczenie Wiekowe")]
        public int ograniczenie_wiekowe { get; set; }



        public ICollection<Seanse> Seanses { get; set; }

        public ICollection<Gatunki_filmy> Gatunkis { get; set; }
        public ICollection<Aktorzy_filmy> Aktorzys { get; set; }
    }
}
