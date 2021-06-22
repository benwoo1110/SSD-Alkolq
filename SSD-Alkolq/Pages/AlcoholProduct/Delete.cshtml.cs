using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.AlcoholProduct
{
    public class DeleteModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DeleteModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AlchoholProduct AlchoholProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlchoholProduct = await _context.AlchoholProduct.FirstOrDefaultAsync(m => m.ID == id);

            if (AlchoholProduct == null)
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

            AlchoholProduct = await _context.AlchoholProduct.FindAsync(id);

            if (AlchoholProduct != null)
            {
                _context.AlchoholProduct.Remove(AlchoholProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
