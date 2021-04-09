using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Pracownicyy
{
    public class KupienieModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public KupienieModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public Bilety Bilet { get; set; }
        public Seanse seanss { get; set; }
        public Klienci klient { get; set; }
        public ICollection<Bilety> Biletys { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Bilet = await _context.Bilety.FirstOrDefaultAsync(m => m.Seanse.seans_id ==id && m.Klienci == null);

            if (Bilet == null)
            {
                return NotFound();
            }
           
            Seanse seans = await _context.Seanse.FirstOrDefaultAsync(m => m.seans_id==id);
            if (seans.ilosc <= 0)
            {
                return RedirectToPage("Stronapracownika");
            }
            seans.ilosc = seans.ilosc-1;

            _context.Attach(seans).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToPage("Stronapracownika");
        }
    }
}
