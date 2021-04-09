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

namespace webapp.Pages.Pracownicyy
{
    public class ZmianaPrawModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public ZmianaPrawModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Klienci Klienci { get; set; }
        [BindProperty]
        public Pracownicy Pracownicy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pracownicy = await _context.Pracownicy.FirstOrDefaultAsync(m => m.Id == id);
            Klienci = new Klienci();
            Klienci.haslo = Pracownicy.haslo;
            Klienci.nr_telefonu = Pracownicy.nr_telefonu;
            Klienci.imie = Pracownicy.imie;
            Klienci.nazwisko = Pracownicy.nazwisko;
            _context.Klienci.Add(Klienci);
            _context.Pracownicy.Remove(Pracownicy);
            await _context.SaveChangesAsync();
            int idd = (int)Klienci.Id;
            return RedirectToPage("/Kliencii/Edit", new { id = idd });
        }
    }
}
