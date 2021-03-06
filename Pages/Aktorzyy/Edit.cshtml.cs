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

namespace webapp.Pages.Aktorzyy
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public EditModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Aktorzy Aktorzy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aktorzy = await _context.Aktorzy.FirstOrDefaultAsync(m => m.aktor_id == id);

            if (Aktorzy == null)
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

            _context.Attach(Aktorzy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AktorzyExists(Aktorzy.aktor_id))
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

        private bool AktorzyExists(int id)
        {
            return _context.Aktorzy.Any(e => e.aktor_id == id);
        }
    }
}
