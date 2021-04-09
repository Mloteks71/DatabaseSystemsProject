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
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public string all;
        public List<Pracownicy> cats;
        public DetailsModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

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
       
            var workers = _context.Pracownicy.Where(item => item.Seanses.Any(j => j.SeanseId == id));
            cats = new List<Pracownicy>(workers);
            foreach (Pracownicy x in cats)
            {
            
                all = all + " " + x.imie+" "+x.nazwisko+" "+x.nr_telefonu+"\n";
            }
            if (Seanse == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
