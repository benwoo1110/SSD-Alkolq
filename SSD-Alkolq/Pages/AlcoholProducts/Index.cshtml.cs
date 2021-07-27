using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    //[Authorize(Roles = "Admin, Users")]
    public class IndexModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public IndexModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<Models.AlcoholProduct> AlchoholProduct { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Type { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AlcoholType { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<string> typeQuery = from m in _context.AlcoholProducts
                                            orderby m.Type
                                            select m.Type;
            var alcohol = from m in _context.AlcoholProducts
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                alcohol = alcohol.Where(s => s.Name.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(AlcoholType))
            {
                alcohol = alcohol.Where(x => x.Type == AlcoholType);
            }
            Type = new SelectList(await typeQuery.Distinct().ToListAsync());
            AlchoholProduct = await alcohol.ToListAsync();
        }
    }
}
