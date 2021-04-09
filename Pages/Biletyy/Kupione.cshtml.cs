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

namespace webapp.Pages.Biletyy
{
    public class KupioneModel : PageModel
    {
        private readonly webapp.Data.KinoContext _context;

        public KupioneModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Bilety> Bilety { get; set; }

        public async Task OnGetAsync()
        {
            var username = HttpContext.Session.GetString("username");
            Bilety = await _context.Bilety.Where(a=>a.Klienci.nr_telefonu.ToString().Equals(username)).ToListAsync();
        }
    }
}
