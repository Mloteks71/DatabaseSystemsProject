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

namespace webapp.Pages.TempU
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public EditModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Klienci).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlienciExists(Klienci.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect("../AdminPanel/");
        }

        private bool KlienciExists(int id)
        {
            return _context.Klienci.Any(e => e.Id == id);
        }
    }
}
