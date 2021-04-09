using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.ListaFilmow
{
    public class Index3Model : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public string A { get; set; }
        public string G { get; set; }
        public List<Aktorzy> listA;
        public List<Gatunki> listG;
        [BindProperty]
        public IList<Filmy> Filmy { get; set; }
        public IList<Filmy> Filmy2 { get; set; }
        public List<Gatunki> pom1;
        public List<Aktorzy> pom2;

        [BindProperty]
        public Aktorzy Aktorzy { get; set; }
        [BindProperty]
        public Gatunki Gatunki { get; set; }

        public Index3Model(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        

        public async Task OnGetAsync(string searchString)
        {
                        
            IQueryable<Filmy> filmTitle = from s in _context.Filmy select s;
            if(!String.IsNullOrEmpty(searchString))
            {
                filmTitle=filmTitle.Where(s => s.tytul.ToLower().Contains(searchString.ToLower()));
            }



            var categoriesg = _context.Gatunki;
            listG = new List<Gatunki>(categoriesg);
            foreach (Gatunki xg in listG)
            {

                G = G + " " + xg.nazwa + "\n";
            }
           
            var categoriesa = _context.Aktorzy;
            listA = new List<Aktorzy>(categoriesa);
            foreach (Aktorzy xa in listA)
            {

                A = A + " " + xa.imie + " " + xa.nazwisko + " " + xa.wiek + " lat " + "\n";
            }
            ViewData["aID"] = new SelectList(listA, "aktor_id", "nazwisko");
            ViewData["gID"] = new SelectList(listG, "gatunek_id", "nazwa");
            Filmy = await _context.Filmy.ToListAsync();
            Filmy = await filmTitle.AsNoTracking().ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            var sth = _context.Gatunki;
            listG = new List<Gatunki>(sth);
            foreach (Gatunki xg in listG)
            {

                G = G + " " + xg.nazwa + "\n";
            }
            var sth1 = _context.Aktorzy;
            listA = new List<Aktorzy>(sth1);
            foreach (Aktorzy xa in listA)
            {

                A = A + " " + xa.imie + " " + xa.nazwisko + " " + xa.wiek + " lat " + "\n";
            }
            ViewData["gID"] = new SelectList(listG, "gatunek_id", "nazwa");
            ViewData["aID"] = new SelectList(listA, "aktor_id", "nazwisko");
            Filmy = await _context.Filmy.ToListAsync();
            Filmy2 = new List<Filmy>();
            switch (submitButton)
            {
                case "Szukaj gatunku":                    
                    foreach (var item in Filmy)
                    {                      
                        var categoriesg = _context.Gatunki.Where(i => i.Filmys.Any(j => j.FilmyId == item.film_id));
                        pom1 = new List<Gatunki>(categoriesg);
                        foreach (Gatunki gatunek in pom1)
                        {
                            if (gatunek.gatunek_id == Gatunki.gatunek_id)
                            {                                
                                Filmy2.Add(item);
                            }                               
                        }
                    }
                    Filmy = Filmy2;
                    return Page();
                case "Szukaj aktora":                    
                    foreach (var item in Filmy)
                    {
                        var categoriesa = _context.Aktorzy.Where(i => i.Filmys.Any(j => j.FilmyId == item.film_id));
                        pom2 = new List<Aktorzy>(categoriesa);
                        foreach (Aktorzy aktor in pom2)                            
                        {
                            if (aktor.aktor_id == Aktorzy.aktor_id)
                            {
                                Filmy2.Add(item);
                            }
                        }
                    }
                    Filmy = Filmy2;
                    return Page();
                default:
                    return RedirectToPage("Index3");
            }
        }
    }
}
