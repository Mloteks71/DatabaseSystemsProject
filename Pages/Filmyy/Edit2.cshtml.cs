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

namespace webapp.Pages.Filmyy
{
    public class Edit2Model : PageModel
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
        public Edit2Model(webapp.Data.KinoContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> OnGetAsync(int? id)
        {

            idd = (int)id;
            var listtg = _context.Gatunki;
            List<Gatunki> catssg = new List<Gatunki>(listtg);
            var categoriesg = _context.Gatunki.Where(item => item.Filmys.Any(j => j.FilmyId == id));
            catsg = new List<Gatunki>(categoriesg);
            foreach (Gatunki xg in catsg)
            {
                if (catssg.Contains(xg))
                { catssg.Remove(xg); }
                allg = allg + " " + xg.nazwa + "\n";
            }

            ViewData["GatID2"] = new SelectList(catsg, "gatunek_id", "nazwa");
            ViewData["GatID"] = new SelectList(catssg, "gatunek_id", "nazwa");



            var listta = _context.Aktorzy;
            List<Aktorzy> catssa = new List<Aktorzy>(listta);
            var categoriesa = _context.Aktorzy.Where(ite => ite.Filmys.Any(jj => jj.FilmyId == id));
            catsa = new List<Aktorzy>(categoriesa);
            foreach (Aktorzy xa in catsa)
            {
                if (catssa.Contains(xa))
                { catssa.Remove(xa); }
                alla = alla + " " + xa.imie + " " + xa.nazwisko + " " + xa.wiek + " lat " + "\n";
            }
            ViewData["AktID2"] = new SelectList(catsa, "aktor_id", "nazwisko");
            ViewData["AktID"] = new SelectList(catssa, "aktor_id", "nazwisko");







        
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            switch (submitButton)
            {
                case "Dodaj Gatunek":
                    {
                        var procatg = new Gatunki_filmy
                        {
                            FilmyId = idd,
                            GatunkiId = Gatunki.gatunek_id
                        };


                        if ((_context.Pracownicy_Seanse.Find(procatg.FilmyId, procatg.GatunkiId)) != null)
                        {

                            return RedirectToPage("Edit2", new { id = idd });
                        }
                        else
                        {
                            _context.Gatunki_filmy.Add(procatg);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Edit2", new { id = idd });
                        }

                    }
                case "Usuñ Gatunek":
                    {

                        var procat = _context.Gatunki_filmy.First(row => row.FilmyId == idd && row.GatunkiId == Gatunki.gatunek_id);

                        _context.Gatunki_filmy.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Edit2", new { id = idd });
                    }
                case "Dodaj Aktora":
                    {
                        var procat = new Aktorzy_filmy
                        {
                            FilmyId = idd,
                            AktorzyId = Aktorzy.aktor_id
                        };


                        if ((_context.Aktorzy_filmy.Find(procat.FilmyId, procat.AktorzyId)) != null)
                        {

                            return RedirectToPage("Edit2", new { id = idd });
                        }
                        else
                        {
                            _context.Aktorzy_filmy.Add(procat);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Edit2", new { id = idd });
                        }

                    }
                case "Usuñ Aktora":
                    {

                        var procat = _context.Aktorzy_filmy.First(row => row.FilmyId == idd && row.AktorzyId == Aktorzy.aktor_id);

                        _context.Aktorzy_filmy.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Edit2", new { id = idd });
                    }

                case "ZatwierdŸ":
                    {
                   

                        return RedirectToPage("./Index");
                    }

                default:
                    return RedirectToPage("Edit2", new { id = idd });
            }
        }

        private bool FilmyExists(int id)
        {
            return _context.Filmy.Any(e => e.film_id == id);
        }
    }
}

