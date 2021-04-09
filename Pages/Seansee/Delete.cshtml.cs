using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Seansee
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DeleteModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Seanse Seanse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seanse = await _context.Seanse.FindAsync(id);
            int g = DateTime.Compare((DateTime)Seanse.data, DateTime.Now);
            if (g<=0)  return RedirectToPage("./Error");
            if (Seanse != null)
            {
                Sale sala = _context.Sale.First(m => m.nr_sali == Seanse.SaleId);
                for(int i=0;i<sala.ilosc_miejsc;i++)
                {
                    Bilety bilet = _context.Bilety.First(m => m.Seanse.seans_id == Seanse.seans_id);
                    _context.Bilety.Remove(bilet);
                    await _context.SaveChangesAsync();
                }
                _context.Seanse.Remove(Seanse);
                await _context.SaveChangesAsync();


            }

            return RedirectToPage("./Index");
        }
    }
}
