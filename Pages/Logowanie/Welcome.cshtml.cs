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

namespace webapp.Pages.Logowanie
{
    public class WelcomeModel : PageModel
    {
        public Klienci klient { get; set; }

        private KinoContext db;
        public WelcomeModel(KinoContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            klient = db.Klienci.SingleOrDefault(a => a.nr_telefonu.ToString().Equals(username));

        }
    }
}
