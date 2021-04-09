using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Filmyy
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public IndexModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Filmy> Filmy { get;set; }

        public async Task OnGetAsync()
        {
            Filmy = await _context.Filmy.ToListAsync();
        }
    }
}
