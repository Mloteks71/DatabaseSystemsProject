using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    [Table("Seanse")]
    public class Seanse
    {
        [Key]
        public int seans_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public Nullable<System.DateTime> data { get; set; }

        public int ilosc { get; set; }

        public ICollection<Bilety> Bilety { get; set; }

        [Display(Name = "Sala")]
        public Sale Sale { get; set; }

        [Display(Name = "Sala")]
        public int SaleId { get; set; }

        [Display(Name = "Film")]
        public Filmy Filmy { get; set; }

        [Display(Name = "Film")]
        public int FilmyId { get; set; }


        public ICollection<Pracownicy_seanse> Pracownicys { get; set; }
    }
}
