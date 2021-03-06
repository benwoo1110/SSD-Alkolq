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
using System.Security.Claims;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    public class DetailsModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DetailsModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public Models.AlcoholProduct AlcoholProduct { get; set; }

        [BindProperty]
        public Models.ProductRating ProductRating { get; set; }

        public IList<ProductRating> ProductRatingList { get; set; }

        public bool ItemInCart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRatingList = await _context.ProductRatings.ToListAsync();

            AlcoholProduct = await _context.AlcoholProducts.FirstOrDefaultAsync(m => m.ID == id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = await _context.ShoppingCart
                .Where(s => s.UserID.Equals(userId))
                .FirstOrDefaultAsync(s => s.AlcoholProductID == AlcoholProduct.ID);
            
            ItemInCart = cartItem != null;

            if (AlcoholProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        //TODO https://stackoverflow.com/questions/55531992/call-server-method-without-page-reload-with-razor-page
        public async Task<IActionResult> OnPostAddToCartAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            var cartItem = new ShoppingCartItem {
                UserID = userId,
                AlcoholProductID = (int) id,
                Quantity = 1
            };

            _context.ShoppingCart.Add(cartItem);
            await _context.SaveChangesAsync();

            return Redirect("~/ShoppingCart");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            ProductRatingList = await _context.ProductRatings.ToListAsync();
            AlcoholProduct = await _context.AlcoholProducts.FirstOrDefaultAsync(m => m.ID == id);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ProductRating.UserID = userId;
            ProductRating.DateTimeStamp = DateTime.Now;
            ProductRating.AlcoholProductID = AlcoholProduct.ID;

            _context.ProductRatings.Add(ProductRating);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = AlcoholProduct.ID });
        }
    }
}
