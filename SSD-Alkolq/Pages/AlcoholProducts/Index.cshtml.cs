using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Alkolq.Pages.AlcoholProduct
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

        public async Task OnGetAsync()
        {
            AlchoholProduct = await _context.AlchoholProduct.ToListAsync();
        }
    }
}
