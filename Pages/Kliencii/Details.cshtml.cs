using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.TempU
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DetailsModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public Klienci Klienci { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Klienci = await _context.Klienci.FirstOrDefaultAsync(m => m.Id == id);

            if (Klienci == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
