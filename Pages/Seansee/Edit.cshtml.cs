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

namespace webapp.Pages.Seansee
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public string all;
        public List<Pracownicy> cats;
        public static int idd;
        [BindProperty]
        public Seanse Seanse { get; set; }

        [BindProperty]
        public Pracownicy Pracownicy { get; set; }
        public EditModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> OnGetAsync(int? id)
        {



            var listt = _context.Pracownicy;
            List<Pracownicy> catss = new List<Pracownicy>(listt);
            var categories = _context.Pracownicy.Where(item => item.Seanses.Any(j => j.SeanseId == id));
            cats = new List<Pracownicy>(categories);
            foreach (Pracownicy x in cats)
            {
                if (catss.Contains(x))
                { catss.Remove(x); }
                all = all + " " + x.imie + " " + x.nazwisko + " " + x.nr_telefonu + "\n";
            }
            idd = (int)id;
            ViewData["WorkID2"] = new SelectList(cats, "Id", "nr_telefonu");
            ViewData["WorkID"] = new SelectList(catss, "Id", "nr_telefonu");
     





            if (id == null)
            {
                return NotFound();
            }

            Seanse = await _context.Seanse
                .Include(s => s.Filmy)
                .Include(s => s.Sale).FirstOrDefaultAsync(m => m.seans_id == id);

            if (Seanse == null)
            {
                return NotFound();
            }
           ViewData["FilmyId"] = new SelectList(_context.Filmy, "film_id", "tytul");
           ViewData["SaleId"] = new SelectList(_context.Sale, "nr_sali", "nr_sali");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {

            switch (submitButton)
            {
                case "Dodaj Pracownika":
                    {
                        var procat = new Pracownicy_seanse
                        {
                            SeanseId = idd,
                            PracownicyId=Pracownicy.Id
                        };


                        if ((_context.Pracownicy_Seanse.Find(procat.SeanseId, procat.PracownicyId)) != null)
                        {

                            return RedirectToPage("Edit", new { id = idd });
                        }
                        else
                        {
                            _context.Pracownicy_Seanse.Add(procat);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Edit", new { id = idd });
                        }

                    }
                case "Usuń Pracownika":
                    {

                        var procat = _context.Pracownicy_Seanse.First(row => row.SeanseId == idd && row.PracownicyId == Pracownicy.pensja);

                        _context.Pracownicy_Seanse.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Edit", new { id = idd });
                    }
                case "Save":
                    {
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }

                        _context.Attach(Seanse).State = EntityState.Modified;

                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!SeanseExists(Seanse.seans_id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }

                        return RedirectToPage("./Index");
                    }
                default:
                    return RedirectToPage("Edit",new { id = idd });



               
            }
        }

        private bool SeanseExists(int id)
        {
            return _context.Seanse.Any(e => e.seans_id == id);
        }
    }
}
