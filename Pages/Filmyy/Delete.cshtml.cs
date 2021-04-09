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
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DeleteModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Filmy Filmy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Filmy = await _context.Filmy.FirstOrDefaultAsync(m => m.film_id == id);

            if (Filmy == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Filmy = await _context.Filmy.FindAsync(id);

            if (Filmy != null)
            {
                _context.Filmy.Remove(Filmy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
