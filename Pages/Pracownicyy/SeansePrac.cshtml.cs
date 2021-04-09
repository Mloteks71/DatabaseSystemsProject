using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Pracownicyy
{
    public class SeansePracModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public SeansePracModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Seanse> Seanse { get; set; }

        public async Task OnGetAsync()
        {
       
            var username = HttpContext.Session.GetString("username");
            var id = _context.Pracownicy.First(b => b.nr_telefonu.ToString().Equals(username));
            int idd = id.Id;
            Seanse = await _context.Seanse.Where(a=>a.Pracownicys.Any(i=>i.PracownicyId.Equals(idd))).ToListAsync();
        }
    }
}
