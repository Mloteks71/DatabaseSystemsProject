using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using webapp.Data;
using webapp.Models;

namespace webapp.Pages
{

    public class LogInModel : PageModel
    {
        [BindProperty]
        public Klienci klient { get; set; }
        public string Msg;
        public bool isPracownik = false;
        private KinoContext db;
        public LogInModel(KinoContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            klient = new Klienci();



        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            var acc = login(klient.nr_telefonu, klient.haslo);
            if (acc == null)
            {
                var prac = loginprac(klient.nr_telefonu, klient.haslo);
                if (prac == null)
                {
                    Msg = "Invalid";
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetString("username", prac.nr_telefonu.ToString());
                    if(prac.isAdmin==true)
                    return RedirectToPage("../Common/MainAdmin");
                    else
                        return RedirectToPage("../Common/MainPrac");
                }
            }
            else
            {
                HttpContext.Session.SetString("username", acc.nr_telefonu.ToString());
                return RedirectToPage("../Common/MainKlient");
            }

        }

        private Klienci login(int username, string password)
        {
            var account = db.Klienci.SingleOrDefault(a => a.nr_telefonu.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.haslo))
                {
                    return account;
                }
            }
            return null;

        }



        private Pracownicy loginprac(int username, string password)
        {
            var account = db.Pracownicy.SingleOrDefault(a => a.nr_telefonu.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.haslo))
                {
                    return account;
                }
            }
            return null;

        }

    }
}
