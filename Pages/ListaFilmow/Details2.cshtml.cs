using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ListaFilmow
{
    public class Details2Model : PageModel
    {      
        private readonly webapp.Data.KinoContext _context;
        public string alla;
        public string allg;
        public List<Aktorzy> catsa;
        public List<Gatunki> catsg;
        public static int idd;
        [BindProperty]
        public Filmy Filmy { get; set; }

        [BindProperty]
        public Aktorzy Aktorzy { get; set; }
        [BindProperty]
        public Gatunki Gatunki { get; set; }
        public Details2Model(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var categoriesg = _context.Gatunki.Where(item => item.Filmys.Any(j => j.FilmyId == id));
            catsg = new List<Gatunki>(categoriesg);
            foreach (Gatunki xg in catsg)
            {

                allg = allg + " " + xg.nazwa + "\n";
            }

            var categoriesa = _context.Aktorzy.Where(ite => ite.Filmys.Any(jj => jj.FilmyId == id));
            catsa = new List<Aktorzy>(categoriesa);
            foreach (Aktorzy xa in catsa)
            {

                alla = alla + " " + xa.imie + " " + xa.nazwisko + " " + xa.wiek + " lat " + "\n";
            }

            if (id == null)
            {
                return NotFound();
            }

            Filmy = await _context.Filmy.FirstOrDefaultAsync(m => m.film_id == id);

            if (Filmy == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
        
