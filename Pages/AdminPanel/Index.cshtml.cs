using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.AdminPanel
{
    public class IndexModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;
        public IndexModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Klienci> Klienci { get; set; }
        public IList<Pracownicy> Pracownicy { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Klienci = await _context.Klienci.ToListAsync();
            Pracownicy = await _context.Pracownicy.ToListAsync();
        }
    }
}
