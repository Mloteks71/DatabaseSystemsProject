using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages.Logowanie
{
    public class SignUpModel : PageModel
    {

        [BindProperty]
        public Klienci klient { get; set; }

        private KinoContext db;
        public string Msg;

        public SignUpModel(KinoContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            klient = new Klienci();
        }
        public IActionResult OnPost()
        {
            var acc = login(klient.nr_telefonu);
            if (acc == false)
            {
                klient.haslo = BCrypt.Net.BCrypt.HashPassword(klient.haslo);

                db.Klienci.Add(klient);
                db.SaveChanges();
                return RedirectToPage("/Index");
            }
            else
            {
                if (acc ==true)
                    Msg = Msg + "Ten Nr telefonu jest juz zarejestrowany \n";
                return Page();
            }

        }

        private bool login(int username)
        {
            var account = db.Klienci.SingleOrDefault(a => a.nr_telefonu.Equals(username));
            if (account != null)
            {
                return true;
            }
            var prac= db.Pracownicy.SingleOrDefault(a => a.nr_telefonu.Equals(username));
            if (prac != null)
            {
                return true;
            }
            return false;
        }

    

    }
}
