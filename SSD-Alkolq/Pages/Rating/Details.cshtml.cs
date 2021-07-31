using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.Rating
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DetailsModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public ProductRating ProductRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRating = await _context.ProductRatings.FirstOrDefaultAsync(m => m.ID == id);

            if (ProductRating == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
