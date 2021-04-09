using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Biletyy
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public Seanse Seanse { get; set; }
        public Seanse Seansee { get; set; }

        public DetailsModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public Bilety Bilety { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }




            Bilety = await _context.Bilety.FirstOrDefaultAsync(m => m.bilet_id == id);
            var seans = _context.Seanse;
            foreach (Seanse s in seans)
            {
                if (s.Bilety.Contains(Bilety))
                {
                    Seansee = s;
                }
            }
            Seanse = await _context.Seanse
             .Include(s => s.Filmy)
             .Include(s => s.Sale).FirstOrDefaultAsync(m => m.seans_id == Seansee.seans_id);
            if (Bilety == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
