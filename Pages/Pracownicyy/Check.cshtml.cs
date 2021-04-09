using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages.Pracownicyy
{
    public class CheckModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        private readonly webapp.Data.KinoContext _context;

        public CheckModel(webapp.Data.KinoContext context)
        {
            _context = context;
        }

        public IList<Klienci> Klienci { get; set; }
        public static int iddd;

        public async Task OnGetAsync(int? id, string searchString)
        {
            if (id != null)
                iddd = (int)id;
            var bilety = _context.Bilety.Where(b => b.Seanse.seans_id == iddd);
             Klienci = _context.Klienci.Where(a => bilety.Any(b => a.Biletys.Any(c => c.bilet_id == b.bilet_id))).ToList();
            
           

       
            int[] xx=new int[Klienci.Count()];
            int i=0;
            int z =Klienci.Count();
            foreach (Klienci x in Klienci)
            {
                foreach(Bilety y in bilety)
                {
                    if (y.Klienci != null)
                    {
                        if (y.Klienci.Id == x.Id)
                        {
                            xx[i]++;
                        }
                    }
                   
                }
                i++;
            }

            List<Klienci> li = new List<Klienci>();
            for (int l = 0; l < z;l++) {
                for (int k = 0; k < xx[l]; k++)
                {
                    li.Add(Klienci[l]);
                  
                }
            }
            Klienci = li;



            IList<Klienci> Klienciii=Klienci;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                 Klienciii = Klienci.Where(p => p.nr_telefonu.ToString().Contains(SearchTerm.ToString())||p.nr_telefonu.ToString().Equals(SearchTerm.ToString())).ToList();
                var x = 3;
            }

           Klienci = await _context.Klienci.ToListAsync();
            Klienci =  Klienciii;


        }
    }
}
