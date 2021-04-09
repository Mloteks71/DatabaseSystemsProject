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
    public class Edit2Model : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public string all;
        public List<Pracownicy> cats;
        public static int idd;

        public Edit2Model(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Seanse Seanse { get; set; }

        [BindProperty]
        public Pracownicy Pracownicy { get; set; }

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

                            return RedirectToPage("Edit2", new { id = idd });
                        }
                        else
                        {
                            _context.Pracownicy_Seanse.Add(procat);
                            await _context.SaveChangesAsync();
                            return RedirectToPage("Edit2", new { id = idd });
                        }

                    }
                case "Usuń Pracownika":
                    {

                        var procat = _context.Pracownicy_Seanse.First(row => row.SeanseId == idd && row.PracownicyId == Pracownicy.pensja);

                        _context.Pracownicy_Seanse.Remove(procat);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("Edit2", new { id = idd });
                    }
                case "Zatwierdź":
                    {
                    

                        return RedirectToPage("./Index");
                    }
                default:
                    return RedirectToPage("Edit2",new { id = idd });



               
            }
        }

        private bool SeanseExists(int id)
        {
            return _context.Seanse.Any(e => e.seans_id == id);
        }
    }
}
