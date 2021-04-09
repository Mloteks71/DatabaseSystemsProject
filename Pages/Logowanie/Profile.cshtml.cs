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
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Klienci klient { get; set; }
        public static int id;

        private KinoContext db;
        public ProfileModel(KinoContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            klient = db.Klienci.SingleOrDefault(a => a.nr_telefonu.ToString().Equals(username));
            id = klient.Id;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(klient.haslo))
            {

                klient.haslo = db.Klienci.AsNoTracking().SingleOrDefault(a => a.nr_telefonu == klient.nr_telefonu).haslo;
            }
            else
            {
               klient.haslo = BCrypt.Net.BCrypt.HashPassword(klient.haslo);
            }
            klient.Id = id;
            db.SaveChanges();
            db.Entry(klient).State = EntityState.Modified;

                db.SaveChanges();
      

        HttpContext.Session.SetString("username", klient.nr_telefonu.ToString());
            return RedirectToPage("Welcome");
        }
     
    }

}
