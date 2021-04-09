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

namespace webapp.Pages.Temp
{
    public class EditModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public EditModel(webapp.Data.KinoContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pracownicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracownicyExists(Pracownicy.Id))
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

        private bool PracownicyExists(int id)
        {
            return _context.Pracownicy.Any(e => e.Id == id);
        }
    }
}
