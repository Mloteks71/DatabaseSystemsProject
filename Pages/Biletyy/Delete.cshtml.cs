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

namespace webapp.Pages.Biletyy
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DeleteModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bilety Bilety { get; set; }
        public Klienci klient { get; set; }
        public Seanse seanss { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bilety = await _context.Bilety.FirstOrDefaultAsync(m => m.bilet_id == id);

            if (Bilety == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            var bil = _context.Bilety.First(a => a.bilet_id == id);
            var seans = _context.Seanse;
            //seanss = _context.Seanse.First(b => b.seans_id.Equals(bil.Seanse.seans_id));
            foreach (Seanse s in seans)
            {
                if (s.Bilety.Contains(bil))
                {
                    seanss = s;
                }
            }
            seanss.ilosc = seanss.ilosc + 1;

            _context.Attach(seanss).State = EntityState.Modified;

            var username = HttpContext.Session.GetString("username");
       





            var kli = _context.Klienci.First(a => a.nr_telefonu.ToString().Equals(username));
            kli.Biletys.Remove(bil);

            await _context.SaveChangesAsync();

            if (Bilety == null)
            {
                return NotFound();
            }
            return RedirectToPage("Kupione");
        }
    }
}
