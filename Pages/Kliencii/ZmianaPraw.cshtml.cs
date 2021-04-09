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

namespace webapp.Pages.Kliencii
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

            Klienci = await _context.Klienci.FirstOrDefaultAsync(m => m.Id == id);
            Pracownicy = new Pracownicy();
            Pracownicy.haslo = Klienci.haslo;
            Pracownicy.nr_telefonu = Klienci.nr_telefonu;
            Pracownicy.imie = Klienci.imie;
            Pracownicy.nazwisko = Klienci.nazwisko;
            Pracownicy.data_zatrudnienia = DateTime.Now;
            Pracownicy.isAdmin = false;
            Pracownicy.pensja = 0;
            Pracownicy.stanowisko = "brak";
            _context.Pracownicy.Add(Pracownicy);
            _context.Klienci.Remove(Klienci);
            await _context.SaveChangesAsync();
            int idd = (int)Pracownicy.Id;
            return RedirectToPage("/Pracownicyy/Edit", new { id = idd });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Klienci).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlienciExists(Klienci.Id))
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

        private bool KlienciExists(int id)
        {
            return _context.Klienci.Any(e => e.Id == id);
        }*/
    }
}
