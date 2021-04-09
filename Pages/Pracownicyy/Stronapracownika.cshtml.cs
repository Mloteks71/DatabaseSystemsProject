using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages
{
    public class StronapracownikaModel : PageModel
    {
        public string CurrentFilter { get; set; }


        private readonly webapp.Data.KinoContext _context;

        public StronapracownikaModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Seanse> Seanses { get; set; }
        public IList<Filmy> Filmy { get; set; }

        public async Task OnGetAsync(string searchString)
        {

            CurrentFilter = searchString;

            IQueryable<Seanse> filmyname = from s in _context.Seanse
                                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                filmyname = filmyname.Where(s => s.Filmy.tytul.ToLower().Contains(searchString.ToLower()));
            }

            Seanses = await _context.Seanse.ToListAsync();
            Filmy = await _context.Filmy.ToListAsync();
            Seanses = await filmyname.ToListAsync();
        }
    }
}
