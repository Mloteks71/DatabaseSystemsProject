using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Temp
{
    public class DeleteModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public DeleteModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pracownicy Pracownicy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pracownicy = await _context.Pracownicy.FirstOrDefaultAsync(m => m.Id == id);

            if (Pracownicy == null)
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

            Pracownicy = await _context.Pracownicy.FindAsync(id);

            if (Pracownicy != null)
            {
                _context.Pracownicy.Remove(Pracownicy);
                await _context.SaveChangesAsync();
            }

            return Redirect("../AdminPanel/");
        }
    }
}
