using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Gatunkii
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public EditModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Gatunki).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatunkiExists(Gatunki.gatunek_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GatunkiExists(int id)
        {
            return _context.Gatunki.Any(e => e.gatunek_id == id);
        }
    }
}
