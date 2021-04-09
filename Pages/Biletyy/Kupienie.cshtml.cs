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

            Seanse seans = await _context.Seanse.FirstOrDefaultAsync(m => m.seans_id == id);
           

            if (seans.ilosc <= 0)
            {
                return RedirectToPage("Kupione");
            }
            seans.ilosc = seans.ilosc - 1;
            Bilet = await _context.Bilety.FirstOrDefaultAsync(m => m.Seanse.seans_id ==id&&m.Klienci==null);
            var username = HttpContext.Session.GetString("username");
            klient = _context.Klienci.SingleOrDefault(a => a.nr_telefonu.ToString().Equals(username));
            if (klient.Biletys == null)
            {
             Biletys = new List<Bilety>();
            }
            else
            {
               Biletys = klient.Biletys;
            }
            Biletys.Add(Bilet);
            klient.Biletys = Biletys;

   

            if (Bilet == null)
            {
                return NotFound();
            }
           
        
            _context.Attach(seans).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToPage("Kupione");
        }
    }
}
