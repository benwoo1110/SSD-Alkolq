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
    public class DeleteModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DeleteModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRating = await _context.ProductRatings.FindAsync(id);

            if (ProductRating != null)
            {
                _context.ProductRatings.Remove(ProductRating);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
