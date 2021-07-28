using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSD_Alkolq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public IndexModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<ProductType> ProductTypes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductTypes = await _context.ProductTypes.ToListAsync();
            return Page();
        }
    }
}
