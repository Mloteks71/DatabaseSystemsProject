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
    public class PrzegladajseanseModel : PageModel
    {
        public string CurrentFilter { get; set; }


        private readonly webapp.Data.KinoContext _context;

        public PrzegladajseanseModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Seanse> Seanse { get; set; }
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

            Seanse = await _context.Seanse.ToListAsync();
            Filmy = await _context.Filmy.ToListAsync();
            Seanse = await filmyname.ToListAsync();
        }
    }
}
