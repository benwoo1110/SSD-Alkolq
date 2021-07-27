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

namespace SSD_Alkolq.Pages.ShoppingCart
{
    public class EditModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public EditModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShoppingCartItem ShoppingCartItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShoppingCartItem = await _context.ShoppingCart
                .Include(s => s.AlcoholProduct).FirstOrDefaultAsync(m => m.ID == id);

            if (ShoppingCartItem == null)
            {
                return NotFound();
            }
           ViewData["AlcoholProductID"] = new SelectList(_context.AlcoholProduct, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cartItem = await _context.ShoppingCart.FindAsync(id);
            if (ShoppingCartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = ShoppingCartItem.Quantity;

            _context.Attach(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
