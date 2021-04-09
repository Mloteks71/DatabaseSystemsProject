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
    public class WelcomePracModel : PageModel
    {
        public Pracownicy prac { get; set; }

        private KinoContext db;
        public WelcomePracModel(KinoContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
           prac = db.Pracownicy.SingleOrDefault(a => a.nr_telefonu.ToString().Equals(username));

        }
    }
}
