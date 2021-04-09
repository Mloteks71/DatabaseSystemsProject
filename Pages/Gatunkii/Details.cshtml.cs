using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Gatunkii
{
    public class DetailsModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DetailsModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public Gatunki Gatunki { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gatunki = await _context.Gatunki.FirstOrDefaultAsync(m => m.gatunek_id == id);

            if (Gatunki == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
