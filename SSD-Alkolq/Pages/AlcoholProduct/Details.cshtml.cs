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
    public class DetailsModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DetailsModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public Models.AlcoholProduct AlcoholProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlcoholProduct = await _context.AlchoholProduct.FirstOrDefaultAsync(m => m.ID == id);

            if (AlcoholProduct == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
