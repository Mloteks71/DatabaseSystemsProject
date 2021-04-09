using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using webapp.Data;
using webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace webapp.Pages.Seansee
{
    public class CreateModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public CreateModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FilmyId"] = new SelectList(_context.Filmy, "film_id", "tytul");
        ViewData["SaleId"] = new SelectList(_context.Sale, "nr_sali", "nr_sali");

            return Page();
        }

        [BindProperty]
        public Seanse Seanse { get; set; }
        [BindProperty]
        public int cena { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int idsali = Seanse.SaleId;
            Sale sala = await _context.Sale.FirstOrDefaultAsync(m => m.nr_sali == idsali);
            Seanse.ilosc = sala.ilosc_miejsc;

            _context.Seanse.Add(Seanse);

            await _context.SaveChangesAsync();
            //Seanse seans = await _context.Seanse.FirstOrDefaultAsync(m => m == Seanse);
          
            for (int i=0; i <sala.ilosc_miejsc; i++)
            {
                Bilety temp = new Bilety();
                temp.cena =cena;
                temp.Seanse = Seanse;
                _context.Bilety.Add(temp);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Edit2", new { id = Seanse.seans_id });
        }
    }
}
